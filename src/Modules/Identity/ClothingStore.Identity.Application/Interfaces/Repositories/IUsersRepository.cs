namespace ClothingStore.Identity.Application.Interfaces.Repositories;

public interface IUsersRepository : IBaseRepository<Guid, User>
{
    Task<User?> GetByMobileWithRolesAsync(MobileNumber mobile, CancellationToken cancellationToken);
    Task<User?> GetByIdWithRolesAsync(Guid userId, CancellationToken cancellationToken);
}