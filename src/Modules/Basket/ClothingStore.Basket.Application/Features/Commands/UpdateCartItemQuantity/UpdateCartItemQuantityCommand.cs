namespace ClothingStore.Basket.Application.Features.Commands.UpdateCartItemQuantity;

public record UpdateCartItemQuantityCommand(Guid ProductId, int Quantity) : ICommand<ErrorOr<CartResponse>>;