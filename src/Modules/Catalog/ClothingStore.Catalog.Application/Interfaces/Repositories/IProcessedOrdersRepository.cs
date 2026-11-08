namespace ClothingStore.Catalog.Application.Interfaces.Repositories;

public interface IProcessedOrdersRepository
{
    Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken);
    Task AddAsync(ProcessedOrder entity, CancellationToken cancellationToken);
}