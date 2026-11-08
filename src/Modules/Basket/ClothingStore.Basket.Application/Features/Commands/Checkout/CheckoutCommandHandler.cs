namespace ClothingStore.Basket.Application.Features.Commands.Checkout;

public class CheckoutCommandHandler(
    ICartsRepository cartsRepository,
    ICurrentUser currentUser,
    ISender sender,
    IOutboxWriter outboxWriter,
    IUnitOfWork unitOfWork) : ICommandHandler<CheckoutCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CheckoutCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, cancellationToken);

        if (cart is null) return CartErrors.CartNotFound;

        if (cart.Items.Count < 1) return CartErrors.CartIsEmpty;

        var productIds = cart.Items.Select(x => x.ProductId).Distinct().ToList();

        var products = await sender.Send(new GetProductsByIdsQuery(productIds), cancellationToken);

        var productsById = products.ToDictionary(x => x.ProductId);

        foreach (var item in cart.Items)
        {
            if (!productsById.TryGetValue(item.ProductId, out var product)) return CartErrors.ProductNotFound(item.ProductName);

            if (!product.IsActive) return CartErrors.NotActive(product.Name);

            if (item.Quantity > product.QuantityInStock) return CartErrors.InsufficientStockForProduct(product.Name);

            if (item.UnitPrice != product.Price) return CartErrors.PriceChanged(product.Name);
        }

        var integrationEvent = new CheckoutIntegrationEvent(
            Guid.NewGuid(),
            Guid.NewGuid(),
            cart.UserId,
            cart.TotalPrice,
            [.. cart.Items.Select(x => new CheckoutItem(x.ProductId, x.ProductName, x.UnitPrice, x.Quantity))]);

        await outboxWriter.AddAsync(integrationEvent, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return integrationEvent.OrderId;
    }
}