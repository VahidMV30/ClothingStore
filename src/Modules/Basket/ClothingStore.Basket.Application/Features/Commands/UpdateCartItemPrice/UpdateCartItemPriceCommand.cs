namespace ClothingStore.Basket.Application.Features.Commands.UpdateCartItemPrice;

public record UpdateCartItemPriceCommand(Guid ProductId, decimal NewPrice) : ICommand<Unit>;