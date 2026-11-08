namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetProductSummaryById;

public class GetProductSummaryByIdQueryHandler(
    IProductsRepository productsRepository) : IQueryHandler<GetProductSummaryByIdQuery, ErrorOr<ProductSummaryResponse>>
{
    public async Task<ErrorOr<ProductSummaryResponse>> Handle(GetProductSummaryByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(query.ProductId, cancellationToken);

        if (product is null) return ProductErrors.NotFound;

        return new ProductSummaryResponse(
            product.Id,
            product.Name,
            product.Price,
            product.QuantityInStock,
            product.IsActive);
    }
}