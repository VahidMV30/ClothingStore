namespace ClothingStore.Catalog.Application.Interfaces.Repositories;

public interface IProductsRepository : IBaseRepository<Guid, Product>
{
    Task<ProductResponse?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponse>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
}