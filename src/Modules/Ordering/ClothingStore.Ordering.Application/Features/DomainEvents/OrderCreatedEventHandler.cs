namespace ClothingStore.Ordering.Application.Features.DomainEvents;

public class OrderCreatedEventHandler(
    IOutboxWriter outboxWriter,
    IUnitOfWork unitOfWork) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var items = notification.Items.Select(x => new OrderCreateItemData(x.ProductId, x.Quantity)).ToList();

        var integrationEvent = new OrderCreatedIntegrationEvent(Guid.NewGuid(), notification.OrderId, notification.UserId, items);

        await outboxWriter.AddAsync(integrationEvent, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);
    }
}