namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductsRepository productsRepository) : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        return await productsRepository.GetAllProductsAsync(cancellationToken);
    }
}