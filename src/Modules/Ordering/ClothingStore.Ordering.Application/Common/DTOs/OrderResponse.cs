namespace ClothingStore.Ordering.Application.Common.DTOs;

public record OrderResponse(Guid OrderId, Guid UserId, string? FullName, string? Mobile, decimal TotalPrice, List<OrderItemResponse> Items);

public record OrderItemResponse(Guid ProductId, string ProductName, decimal Price, int Quantity);