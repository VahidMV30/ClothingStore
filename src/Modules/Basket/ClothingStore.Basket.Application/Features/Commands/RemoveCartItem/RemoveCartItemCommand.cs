namespace ClothingStore.Basket.Application.Features.Commands.RemoveCartItem;

public record RemoveCartItemCommand(Guid ProductId) : ICommand<CartResponse>;