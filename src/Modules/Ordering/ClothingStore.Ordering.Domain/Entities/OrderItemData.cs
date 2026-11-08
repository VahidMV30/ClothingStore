namespace ClothingStore.Ordering.Domain.Entities;

public record OrderItemData(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);