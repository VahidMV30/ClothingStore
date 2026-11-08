namespace ClothingStore.Ordering.Application.Features.Queries.GetMyOrders;

public class GetMyOrdersQueryHandler(
    IOrdersRepository ordersRepository,
    ICurrentUser currentUser) : IQueryHandler<GetMyOrdersQuery, List<MyOrderResponse>>
{
    public async Task<List<MyOrderResponse>> Handle(GetMyOrdersQuery command, CancellationToken cancellationToken)
    {
        return await ordersRepository.GetMyOrdersAsync(currentUser.Id, cancellationToken);
    }
}