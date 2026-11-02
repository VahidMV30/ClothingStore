namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductsRepository productsRepository) : IQueryHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetProductByIdAsync(query.Id, cancellationToken);

        if (product is null) return ProductErrors.NotFound;

        return product;
    }
}