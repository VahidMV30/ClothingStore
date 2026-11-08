namespace ClothingStore.Catalog.Infrastructure.Messaging.Consumers;

public class OrderCreatedConsumer(ISender sender) : IConsumer<OrderCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var items = message.Items.Select(x => new DecreaseStockItems(x.ProductId, x.Quantity)).ToList();

        await sender.Send(new DecreaseStockForOrderCommand(message.OrderId, items), context.CancellationToken);
    }
}