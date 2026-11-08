namespace ClothingStore.Basket.Application.Features.Commands.RemoveCartItem;

public class RemoveCartItemCommandHandler(
    ICartsRepository cartsRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveCartItemCommand, CartResponse>
{
    public async Task<CartResponse> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, cancellationToken);

        if (cart is null) return new CartResponse(Guid.Empty, [], 0);

        cart.RemoveItem(command.ProductId);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return cart.Adapt<CartResponse>();
    }
}