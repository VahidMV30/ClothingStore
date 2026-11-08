namespace ClothingStore.Basket.Application.Features.Commands.ClearCartForOrder;

public class ClearCartForOrderCommandHandler(
    ICartsRepository cartsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ClearCartForOrderCommand, Unit>
{
    public async Task<Unit> Handle(ClearCartForOrderCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(command.UserId, cancellationToken);

        if (cart is null || cart.Items.Count == 0) return Unit.Value;

        cart.ClearCartItems();

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Unit.Value;
    }
}