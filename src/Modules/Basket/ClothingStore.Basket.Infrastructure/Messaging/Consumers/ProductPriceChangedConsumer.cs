namespace ClothingStore.Basket.Infrastructure.Messaging.Consumers;

public class ProductPriceChangedConsumer(ISender sender) : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        var message = context.Message;

        await sender.Send(new UpdateCartItemPriceCommand(message.ProductId, message.NewPrice), context.CancellationToken);
    }
}