namespace ClothingStore.Basket.Infrastructure.Messaging.Consumers;

public class ProductDeactivatedConsumer(ISender sender) : IConsumer<ProductDeactivatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductDeactivatedIntegrationEvent> context)
    {
        var message = context.Message;

        await sender.Send(new RemoveDeactivatedCartItemCommand(message.ProductId), context.CancellationToken);
    }
}