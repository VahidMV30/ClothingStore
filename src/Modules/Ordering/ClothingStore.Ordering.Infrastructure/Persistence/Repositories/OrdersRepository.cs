namespace ClothingStore.Ordering.Infrastructure.Persistence.Repositories;

public class OrdersRepository(OrderingDbContext dbContext) : BaseRepository<Guid, Order>(dbContext), IOrdersRepository
{
    public async Task<List<MyOrderResponse>> GetMyOrdersAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new MyOrderResponse(
                x.Id,
                x.UserId,
                x.Items.Sum(t => t.UnitPrice * t.Quantity),
                x.Items.Select(y => new MyOrderItemResponse(y.ProductId, y.ProductName, y.UnitPrice, y.Quantity)).ToList()))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<OrderResponse>> GetAllOrdersAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Select(x => new OrderResponse(
                x.Id,
                x.UserId,
                null,
                null,
                x.Items.Sum(t => t.UnitPrice * t.Quantity),
                x.Items.Select(y => new OrderItemResponse(y.ProductId, y.ProductName, y.UnitPrice, y.Quantity)).ToList()))
            .ToListAsync(cancellationToken);
    }
}