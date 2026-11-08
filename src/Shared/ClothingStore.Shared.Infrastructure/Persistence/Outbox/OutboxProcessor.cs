namespace ClothingStore.Shared.Infrastructure.Persistence.Outbox;

public class OutboxProcessor<TDbContext>(
    IServiceScopeFactory serviceScopeFactory,
    TimeProvider timeProvider,
    ILogger<OutboxProcessor<TDbContext>> logger) : BackgroundService where TDbContext : DbContext
{
    private const int BatchSize = 20;
    private const int MaxRetries = 5;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var time = new PeriodicTimer(TimeSpan.FromSeconds(10));

        do
        {
            try
            {
                await ProcessBatchAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Outbox processing failed.");
            }
        } while (await time.WaitForNextTickAsync(stoppingToken));
    }

    public async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var messages = await dbContext.Set<OutboxMessage>()
            .Where(x => x.ProcessedAt == null && x.RetryCount < MaxRetries)
            .OrderBy(x => x.CreatedAt)
            .Take(BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
            try
            {
                var type = Type.GetType(message.Type) ?? throw new InvalidOperationException($"Type not found: {message.Type}");

                var integrationEvent = JsonSerializer.Deserialize(message.Content, type) ??
                                       throw new InvalidOperationException("Deserialize returned null");

                await publishEndpoint.Publish(integrationEvent, type, ctx => ctx.MessageId = message.Id, cancellationToken);

                message.MarkProcessed(timeProvider.GetUtcNow().UtcDateTime);
            }
            catch (Exception ex)
            {
                var error = ex.Message.Length > 2048 ? ex.Message[..2048] : ex.Message;
                message.MarkFailed(error);
                logger.LogWarning(ex, "Failed to publish outbox message {MessageId}", message.Id);
            }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}