namespace ClothingStore.Catalog.Application.Features.Products.DomainEvents;

public class ProductPriceChangedEventHandler(IOutboxWriter outboxWriter) : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ProductPriceChangedIntegrationEvent(Guid.NewGuid(), notification.ProductId, notification.NewPrice);

        await outboxWriter.AddAsync(integrationEvent, cancellationToken);
    }
}