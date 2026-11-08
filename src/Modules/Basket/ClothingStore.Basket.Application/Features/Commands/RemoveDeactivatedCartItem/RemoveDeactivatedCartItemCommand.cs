namespace ClothingStore.Basket.Application.Features.Commands.RemoveDeactivatedCartItem;

public record RemoveDeactivatedCartItemCommand(Guid ProductId) : ICommand<Unit>;