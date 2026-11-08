namespace ClothingStore.Basket.Infrastructure.Messaging.Consumers;

public class OrderCreatedConsumer(ISender sender) : IConsumer<OrderCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        await sender.Send(new ClearCartForOrderCommand(message.UserId));
    }
}