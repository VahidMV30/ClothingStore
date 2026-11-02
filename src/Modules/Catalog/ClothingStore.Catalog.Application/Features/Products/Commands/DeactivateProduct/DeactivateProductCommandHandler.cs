namespace ClothingStore.Catalog.Application.Features.Products.Commands.DeactivateProduct;

public class DeactivateProductCommandHandler(
    IProductsRepository productsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeactivateProductCommand, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(DeactivateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(command.ProductId, cancellationToken);

        if (product is null) return ProductErrors.NotFound;

        product.Deactivate();

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return product.Adapt<ProductResponse>();
    }
}