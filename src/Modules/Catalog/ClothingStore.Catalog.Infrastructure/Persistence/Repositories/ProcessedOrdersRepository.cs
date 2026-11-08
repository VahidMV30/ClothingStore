namespace ClothingStore.Catalog.Infrastructure.Persistence.Repositories;

public class ProcessedOrdersRepository(CatalogDbContext dbContext) : IProcessedOrdersRepository
{
    public async Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await dbContext.ProcessedOrders.AnyAsync(x => x.OrderId == orderId, cancellationToken);
    }

    public async Task AddAsync(ProcessedOrder entity, CancellationToken cancellationToken)
    {
        await dbContext.ProcessedOrders.AddAsync(entity, cancellationToken);
    }
}