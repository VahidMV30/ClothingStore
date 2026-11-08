namespace ClothingStore.Ordering.Application.Features.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrdersRepository ordersRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Unit>
{
    public async Task<Unit> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        if (await ordersRepository.ExistsAsync(x => x.Id == command.OrderId, cancellationToken)) return Unit.Value;

        var items = command.Items
            .Select(x => new OrderItemData(x.ProductId, x.ProductName, x.UnitPrice, x.Quantity))
            .ToList();

        var order = Order.Create(command.OrderId, command.UserId, items);

        await ordersRepository.AddAsync(order, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Unit.Value;
    }
}