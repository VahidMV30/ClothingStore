namespace ClothingStore.Ordering.Application.Common.DTOs;

public record MyOrderResponse(Guid OrderId, Guid UserId, decimal TotalPrice, IReadOnlyList<MyOrderItemResponse> Items);

public record MyOrderItemResponse(Guid ProductId, string ProductName, decimal Price, int Quantity);