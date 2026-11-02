namespace ClothingStore.Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductsRepository productsRepository,
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        command = command with { Name = command.Name.Trim(), ImageUrl = command.ImageUrl.Trim(), Description = command.Description.Trim() };

        if (await productsRepository.ExistsAsync(x => x.Name == command.Name, cancellationToken)) return ProductErrors.Duplicated;

        var category = await categoriesRepository.GetByIdAsync(command.CategoryId, cancellationToken);

        if (category is null) return CategoryErrors.NotFound;

        var product = Product.Create
        (Guid.NewGuid(),
            command.CategoryId,
            command.Name,
            command.Price,
            command.QuantityInStock,
            command.ImageUrl,
            command.Description);

        await productsRepository.AddAsync(product, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return product.Adapt<ProductResponse>();
    }
}