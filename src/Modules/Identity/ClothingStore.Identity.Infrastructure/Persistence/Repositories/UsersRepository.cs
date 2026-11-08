namespace ClothingStore.Identity.Infrastructure.Persistence.Repositories;

public class UsersRepository(IdentityDbContext dbContext) : BaseRepository<Guid, User>(dbContext), IUsersRepository
{
    public async Task<User?> GetByMobileWithRolesAsync(MobileNumber mobile, CancellationToken cancellationToken)
    {
        return await dbContext.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Mobile == mobile, cancellationToken);
    }

    public async Task<User?> GetByIdWithRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Include(x => x.UserRoles)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task<List<UserSummaryResponse>> GetUserSummaryByIdsAsync(List<Guid> userIds, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .Where(x => userIds.Contains(x.Id))
            .Select(x => new UserSummaryResponse(x.Id, x.FullName, x.Mobile.Value))
            .ToListAsync(cancellationToken);
    }
}