namespace ClothingStore.Catalog.Application.Features.Products.Commands.ActivateProduct;

public class ActivateProductCommandHandler(
    IProductsRepository productsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<ActivateProductCommand, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(ActivateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(command.ProductId, cancellationToken);

        if (product is null) return ProductErrors.NotFound;

        product.Activate();

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return product.Adapt<ProductResponse>();
    }
}