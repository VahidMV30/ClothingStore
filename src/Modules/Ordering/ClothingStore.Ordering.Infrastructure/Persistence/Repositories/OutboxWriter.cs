namespace ClothingStore.Ordering.Infrastructure.Persistence.Repositories;

public class OutboxWriter(OrderingDbContext dbContext, TimeProvider timeProvider) : IOutboxWriter
{
    public async Task AddAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        await dbContext.OutboxMessages.AddAsync(OutboxMessage.Create(integrationEvent, timeProvider.GetUtcNow().UtcDateTime), cancellationToken);
    }
}