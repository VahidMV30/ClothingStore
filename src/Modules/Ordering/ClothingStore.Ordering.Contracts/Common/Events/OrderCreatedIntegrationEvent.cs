namespace ClothingStore.Ordering.Contracts.Common.Events;

public record OrderCreatedIntegrationEvent(Guid MessageId, Guid OrderId, Guid UserId, IReadOnlyList<OrderCreateItemData> Items) : IIntegrationEvent;

public record OrderCreateItemData(Guid ProductId, int Quantity);