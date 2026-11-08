namespace ClothingStore.Catalog.Domain.Events;

public record ProductPriceChangedEvent(Guid ProductId, decimal NewPrice) : DomainEvent;