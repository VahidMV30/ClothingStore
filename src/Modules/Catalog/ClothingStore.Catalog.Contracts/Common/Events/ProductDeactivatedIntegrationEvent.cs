namespace ClothingStore.Catalog.Contracts.Common.Events;

public record ProductDeactivatedIntegrationEvent(Guid MessageId, Guid ProductId) : IIntegrationEvent;