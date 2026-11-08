namespace ClothingStore.Basket.Application.Common.DTOs;

public record AddItemToCartRequest(Guid ProductId, int Quantity);