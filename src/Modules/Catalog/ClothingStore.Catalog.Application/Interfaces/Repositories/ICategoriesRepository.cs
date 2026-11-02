namespace ClothingStore.Catalog.Application.Interfaces.Repositories;

public interface ICategoriesRepository : IBaseRepository<Guid, Category>
{
    Task<CategoryResponse?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync(CancellationToken cancellationToken);
}