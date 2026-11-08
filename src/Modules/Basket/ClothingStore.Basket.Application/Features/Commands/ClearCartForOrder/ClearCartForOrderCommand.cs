namespace ClothingStore.Basket.Application.Features.Commands.ClearCartForOrder;

public record ClearCartForOrderCommand(Guid UserId) : ICommand<Unit>;