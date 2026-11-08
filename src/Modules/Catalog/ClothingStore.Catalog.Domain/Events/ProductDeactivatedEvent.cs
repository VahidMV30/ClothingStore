namespace ClothingStore.Catalog.Domain.Events;

public record ProductDeactivatedEvent(Guid ProductId) : DomainEvent;