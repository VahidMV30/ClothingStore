namespace ClothingStore.Ordering.Application.Interfaces.Repositories;

public interface IOrdersRepository : IBaseRepository<Guid, Order>
{
    Task<List<MyOrderResponse>> GetMyOrdersAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellationToken);
}