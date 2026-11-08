namespace ClothingStore.Basket.Application.Features.Commands.RemoveDeactivatedCartItem;

public class RemoveDeactivatedCartItemCommandHandler(
    ICartsRepository cartsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveDeactivatedCartItemCommand, Unit>
{
    public async Task<Unit> Handle(RemoveDeactivatedCartItemCommand command, CancellationToken cancellationToken)
    {
        var carts = await cartsRepository.GetByProductIdAsync(command.ProductId, cancellationToken);

        foreach (var cart in carts) cart.RemoveItem(command.ProductId);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Unit.Value;
    }
}