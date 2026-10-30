namespace ClothingStore.Identity.Infrastructure.Persistence.Repositories;

public class RolesRepository(IdentityDbContext dbContext) : BaseRepository<Guid, Role>(dbContext), IRolesRepository
{
    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<List<Role>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await dbContext.Roles.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}