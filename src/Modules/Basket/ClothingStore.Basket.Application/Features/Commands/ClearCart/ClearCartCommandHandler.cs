namespace ClothingStore.Basket.Application.Features.Commands.ClearCart;

public class ClearCartCommandHandler(
    ICartsRepository cartsRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    ICartCache cache) : ICommandHandler<ClearCartCommand, CartResponse>
{
    public async Task<CartResponse> Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, false, cancellationToken);

        if (cart is null) return new CartResponse(Guid.Empty, [], 0);

        cart.ClearCartItems();

        await unitOfWork.CommitChangesAsync(cancellationToken);

        await cache.InvalidateAsync(cart.UserId, cancellationToken);

        return cart.Adapt<CartResponse>();
    }
}