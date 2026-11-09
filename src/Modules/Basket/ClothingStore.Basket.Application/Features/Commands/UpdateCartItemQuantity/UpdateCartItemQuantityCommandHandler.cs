namespace ClothingStore.Basket.Application.Features.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandHandler(
    ISender sender,
    ICartsRepository cartsRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    ICartCache cache) : ICommandHandler<UpdateCartItemQuantityCommand, ErrorOr<CartResponse>>
{
    public async Task<ErrorOr<CartResponse>> Handle(UpdateCartItemQuantityCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, false, cancellationToken);
        if (cart is null) return CartErrors.CartNotFound;

        if (cart.Items.All(x => x.ProductId != command.ProductId)) return CartErrors.ProductNotInCart;

        var productResult = await sender.Send(new GetProductSummaryByIdQuery(command.ProductId), cancellationToken);
        if (productResult.IsError) return productResult.Errors;
        var product = productResult.Value;

        if (product.QuantityInStock < command.Quantity) return CartErrors.InsufficientStock;

        cart.UpdateCartItemQuantity(command.ProductId, command.Quantity);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        await cache.InvalidateAsync(cart.UserId, cancellationToken);

        return cart.Adapt<CartResponse>();
    }
}