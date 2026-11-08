namespace ClothingStore.Basket.Application.Features.Commands.ClearCart;

public class ClearCartCommandHandler(
    ICartsRepository cartsRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : ICommandHandler<ClearCartCommand, CartResponse>
{
    public async Task<CartResponse> Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, cancellationToken);

        if (cart is null) return new CartResponse(Guid.Empty, [], 0);

        cart.ClearCartItems();

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return cart.Adapt<CartResponse>();
    }
}