namespace ClothingStore.Ordering.Domain.Events;

public record OrderCreatedEvent(Guid OrderId, Guid UserId, List<OrderCreatedItem> Items) : DomainEvent;

public record OrderCreatedItem(Guid ProductId, int Quantity);