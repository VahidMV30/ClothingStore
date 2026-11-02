namespace ClothingStore.Catalog.Infrastructure.Persistence.Repositories;

public class CategoriesRepository(CatalogDbContext dbContext) : BaseRepository<Guid, Category>(dbContext), ICategoriesRepository
{
    public async Task<CategoryResponse?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CategoryResponse(x.Id, x.Name, x.Description))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .Select(x => new CategoryResponse(x.Id, x.Name, x.Description))
            .ToListAsync(cancellationToken);
    }
}