namespace ClothingStore.Identity.Application.Features.Queries.Me;

public class MeQueryHandler(
    ICurrentUser currentUser,
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository) : IQueryHandler<MeQuery, ErrorOr<UserResponse>>
{
    public async Task<ErrorOr<UserResponse>> Handle(MeQuery command, CancellationToken cancellationToken)
    {
        if (currentUser.Id is not { } userId) return AuthErrors.Unauthorized;

        var user = await usersRepository.GetByIdWithRolesAsync(userId, cancellationToken);

        if (user is null) return AuthErrors.UserNotFound;

        var roleIds = user.UserRoles.Select(x => x.RoleId).ToList();
        var roles = await rolesRepository.GetByIdsAsync(roleIds, cancellationToken);

        return new UserResponse(user.Id, user.FullName, user.Mobile.Value, [.. roles.Select(x => x.Name)]);
    }
}