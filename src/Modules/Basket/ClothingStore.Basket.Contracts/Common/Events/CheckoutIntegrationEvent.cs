namespace ClothingStore.Basket.Contracts.Common.Events;

public record CheckoutIntegrationEvent(Guid MessageId, Guid OrderId, Guid UserId, decimal TotalPrice, List<CheckoutItem> Items) : IIntegrationEvent;

public record CheckoutItem(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);