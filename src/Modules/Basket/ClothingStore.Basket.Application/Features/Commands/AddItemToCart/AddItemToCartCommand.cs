namespace ClothingStore.Basket.Application.Features.Commands.AddItemToCart;

public record AddItemToCartCommand(Guid ProductId, int Quantity) : ICommand<ErrorOr<CartResponse>>;