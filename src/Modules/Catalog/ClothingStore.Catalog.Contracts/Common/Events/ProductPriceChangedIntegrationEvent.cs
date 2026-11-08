namespace ClothingStore.Catalog.Contracts.Common.Events;

public record ProductPriceChangedIntegrationEvent(Guid MessageId, Guid ProductId, decimal NewPrice) : IIntegrationEvent;