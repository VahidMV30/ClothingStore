namespace ClothingStore.Basket.Application.Features.Commands.UpdateCartItemPrice;

public class UpdateCartItemPriceCommandHandler(
    ICartsRepository cartsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCartItemPriceCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCartItemPriceCommand command, CancellationToken cancellationToken)
    {
        var carts = await cartsRepository.GetByProductIdAsync(command.ProductId, cancellationToken);

        foreach (var cart in carts) cart.UpdateCartItemPrice(command.ProductId, command.NewPrice);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Unit.Value;
    }
}