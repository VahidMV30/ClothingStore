namespace ClothingStore.Ordering.Infrastructure.Messaging.Consumers;

public class CheckoutConsumer(ISender sender) : IConsumer<CheckoutIntegrationEvent>
{
    public async Task Consume(ConsumeContext<CheckoutIntegrationEvent> context)
    {
        var message = context.Message;

        await sender.Send(new CreateOrderCommand(message.OrderId, message.UserId, [
            .. message.Items.Select(x => new CreateOrderItems(
                x.ProductId
                , x.ProductName,
                x.UnitPrice,
                x.Quantity))
        ]));
    }
}