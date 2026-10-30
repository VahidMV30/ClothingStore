namespace ClothingStore.Identity.Application.Interfaces.Repositories;

public interface IRolesRepository : IBaseRepository<Guid, Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<List<Role>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}