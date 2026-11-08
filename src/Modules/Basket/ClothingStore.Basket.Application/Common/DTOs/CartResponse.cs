namespace ClothingStore.Basket.Application.Common.DTOs;

public record CartResponse(Guid Id, IReadOnlyList<CartItemResponse> Items, decimal TotalPrice);

public record CartItemResponse(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);