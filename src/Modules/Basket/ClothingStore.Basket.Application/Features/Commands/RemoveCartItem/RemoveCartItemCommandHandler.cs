namespace ClothingStore.Basket.Application.Features.Commands.RemoveCartItem;

public class RemoveCartItemCommandHandler(
    ICartsRepository cartsRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    ICartCache cache) : ICommandHandler<RemoveCartItemCommand, CartResponse>
{
    public async Task<CartResponse> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, false, cancellationToken);

        if (cart is null) return new CartResponse(Guid.Empty, [], 0);

        cart.RemoveItem(command.ProductId);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        await cache.InvalidateAsync(cart.UserId, cancellationToken);

        return cart.Adapt<CartResponse>();
    }
}