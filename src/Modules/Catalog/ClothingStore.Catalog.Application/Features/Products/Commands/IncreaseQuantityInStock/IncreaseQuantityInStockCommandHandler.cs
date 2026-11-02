namespace ClothingStore.Catalog.Application.Features.Products.Commands.IncreaseQuantityInStock;

public class IncreaseQuantityInStockCommandHandler(
    IProductsRepository productsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<IncreaseQuantityInStockCommand, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(IncreaseQuantityInStockCommand inStockCommand, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(inStockCommand.ProductId, cancellationToken);

        if (product is null) return ProductErrors.NotFound;

        product.IncreaseQuantityInStock(inStockCommand.Quantity);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return product.Adapt<ProductResponse>();
    }
}