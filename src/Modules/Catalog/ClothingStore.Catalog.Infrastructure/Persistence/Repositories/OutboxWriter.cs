namespace ClothingStore.Catalog.Infrastructure.Persistence.Repositories;

public class OutboxWriter(CatalogDbContext dbContext, TimeProvider timeProvider) : IOutboxWriter
{
    public async Task AddAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(OutboxMessage.Create(integrationEvent, timeProvider.GetUtcNow().UtcDateTime), cancellationToken);
    }
}