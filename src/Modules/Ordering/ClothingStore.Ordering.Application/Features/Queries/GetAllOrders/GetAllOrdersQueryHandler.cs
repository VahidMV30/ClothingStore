namespace ClothingStore.Ordering.Application.Features.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(IOrdersRepository ordersRepository, ISender sender) : IQueryHandler<GetAllOrdersQuery, List<OrderResponse>>
{
    public async Task<List<OrderResponse>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await ordersRepository.GetAllOrdersAsync(cancellationToken);

        var userIds = orders.Select(o => o.UserId).Distinct().ToList();

        var users = await sender.Send(new GetUserSummaryByIdsQuery(userIds), cancellationToken);

        var userById = users.ToDictionary(x => x.UserId);

        return
        [
            .. orders.Select(order =>
            {
                userById.TryGetValue(order.UserId, out var user);

                return order with { FullName = user?.FullName, Mobile = user?.Mobile };
            })
        ];
    }
}