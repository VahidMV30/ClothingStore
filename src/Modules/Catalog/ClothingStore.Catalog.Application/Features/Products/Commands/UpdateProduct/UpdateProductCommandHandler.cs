namespace ClothingStore.Catalog.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductsRepository productsRepository,
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        command = command with { Name = command.Name.Trim(), ImageUrl = command.ImageUrl.Trim(), Description = command.Description.Trim() };

        var product = await productsRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product is null) return ProductErrors.NotFound;

        var category = await categoriesRepository.GetByIdAsync(command.CategoryId, cancellationToken);
        if (category is null) return CategoryErrors.NotFound;

        if (await productsRepository.ExistsAsync(x => x.Name == command.Name && x.Id != command.Id, cancellationToken))
            return ProductErrors.Duplicated;

        product.Update(command.CategoryId, command.Name, command.Price, command.QuantityInStock, command.ImageUrl, command.Description);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return product.Adapt<ProductResponse>();
    }
}