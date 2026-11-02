namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetProductsByCategoryId;

public class GetProductsByCategoryIdQueryHandler(
    IProductsRepository productsRepository) : IQueryHandler<GetProductsByCategoryIdQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByCategoryIdQuery query, CancellationToken cancellationToken)
    {
        return await productsRepository.GetProductsByCategoryIdAsync(query.CategoryId, cancellationToken);
    }
}