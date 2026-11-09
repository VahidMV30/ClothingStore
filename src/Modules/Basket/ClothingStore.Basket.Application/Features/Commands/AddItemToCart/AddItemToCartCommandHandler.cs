namespace ClothingStore.Basket.Application.Features.Commands.AddItemToCart;

public class AddItemToCartCommandHandler(
    ICurrentUser currentUser,
    ICartsRepository cartsRepository,
    IUnitOfWork unitOfWork,
    ISender sender,
    ICartCache cache) : ICommandHandler<AddItemToCartCommand, ErrorOr<CartResponse>>
{
    public async Task<ErrorOr<CartResponse>> Handle(AddItemToCartCommand command, CancellationToken cancellationToken)
    {
        var productResult = await sender.Send(new GetProductSummaryByIdQuery(command.ProductId), cancellationToken);

        if (productResult.IsError) return productResult.Errors;

        var product = productResult.Value;

        if (!product.IsActive) return CartErrors.ProductNotActive;

        var userId = currentUser.Id;

        var cart = await cartsRepository.GetByUserIdAsync(userId, false, cancellationToken);

        var isNewCart = cart is null;

        cart ??= Cart.Create(Guid.NewGuid(), userId);

        var currentQuantity = cart.Items.FirstOrDefault(x => x.ProductId == command.ProductId)?.Quantity ?? 0;

        if (currentQuantity + command.Quantity > product.QuantityInStock) return CartErrors.InsufficientStock;

        cart.AddItem(product.ProductId, product.Name, product.Price, command.Quantity);

        if (isNewCart) await cartsRepository.AddAsync(cart, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        await cache.InvalidateAsync(cart.UserId, cancellationToken);

        return cart.Adapt<CartResponse>();
    }
}