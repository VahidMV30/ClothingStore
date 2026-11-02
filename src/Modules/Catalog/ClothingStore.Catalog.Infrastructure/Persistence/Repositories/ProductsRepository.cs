namespace ClothingStore.Catalog.Infrastructure.Persistence.Repositories;

public class ProductsRepository(CatalogDbContext dbContext) : BaseRepository<Guid, Product>(dbContext), IProductsRepository
{
    public async Task<ProductResponse?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == productId)
            .Select(x => new ProductResponse(
                x.Id,
                x.CategoryId,
                x.Name,
                x.Price,
                x.QuantityInStock,
                x.ImageUrl,
                x.Description,
                x.IsActive
            )).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .AsNoTracking()
            .Select(x => new ProductResponse(
                x.Id,
                x.CategoryId,
                x.Name,
                x.Price,
                x.QuantityInStock,
                x.ImageUrl,
                x.Description,
                x.IsActive
            )).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(x => x.CategoryId == categoryId)
            .Select(x => new ProductResponse(
                x.Id,
                x.CategoryId,
                x.Name,
                x.Price,
                x.QuantityInStock,
                x.ImageUrl,
                x.Description,
                x.IsActive
            )).ToListAsync(cancellationToken);
    }
}