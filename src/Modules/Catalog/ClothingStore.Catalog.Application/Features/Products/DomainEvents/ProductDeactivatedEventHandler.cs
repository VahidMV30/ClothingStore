namespace ClothingStore.Catalog.Application.Features.Products.DomainEvents;

public class ProductDeactivatedEventHandler(IOutboxWriter outboxWriter) : INotificationHandler<ProductDeactivatedEvent>
{
    public async Task Handle(ProductDeactivatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ProductDeactivatedIntegrationEvent(Guid.NewGuid(), notification.ProductId);

        await outboxWriter.AddAsync(integrationEvent, cancellationToken);
    }
}