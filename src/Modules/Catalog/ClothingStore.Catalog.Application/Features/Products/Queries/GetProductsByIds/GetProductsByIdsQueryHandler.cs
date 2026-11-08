namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetProductsByIds;

public class GetProductsByIdsQueryHandler(IProductsRepository productsRepository)
    : IQueryHandler<GetProductsByIdsQuery, List<ProductSummaryResponse>>
{
    public async Task<List<ProductSummaryResponse>> Handle(GetProductsByIdsQuery query, CancellationToken cancellationToken)
    {
        var products = await productsRepository.GetProductsByIdsAsync(query.ProductIds, cancellationToken);

        return products;
    }
}