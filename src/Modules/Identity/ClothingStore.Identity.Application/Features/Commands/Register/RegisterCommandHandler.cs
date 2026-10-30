namespace ClothingStore.Identity.Application.Features.Commands.Register;

public class RegisterCommandHandler(
    IUsersRepository usersRepository,
    IRolesRepository rolesRepository,
    ISecurityService securityService,
    TimeProvider timeProvider,
    IRefreshTokensRepository refreshTokensRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterCommand, ErrorOr<AuthResponse>>
{
    public async Task<ErrorOr<AuthResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var mobile = MobileNumber.Create(command.Mobile);

        if (await usersRepository.ExistsAsync(x => x.Mobile == mobile, cancellationToken)) return AuthErrors.AlreadyRegistered;

        var userRole = await rolesRepository.GetByNameAsync(ApplicationRoles.User, cancellationToken);

        var user = User.Create(Guid.NewGuid(), command.FullName, mobile, securityService.HashPassword(command.Password));
        user.AssignRole(Guid.NewGuid(), userRole!.Id);

        await usersRepository.AddAsync(user, cancellationToken);

        var now = timeProvider.GetUtcNow().UtcDateTime;

        var accessToken = securityService.GenerateAccessToken(user.Id, user.FullName, user.Mobile.Value, [userRole.Name]);

        var (refreshToken, hashedRefreshToken) = securityService.GenerateRefreshToken();

        var newRefreshToken = RefreshToken.Create(Guid.NewGuid(), user.Id, hashedRefreshToken, now, securityService.RefreshTokenLifetime);

        await refreshTokensRepository.AddAsync(newRefreshToken, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return new AuthResponse(accessToken, refreshToken, now.Add(securityService.AccessTokenLifetime), newRefreshToken.ExpiresAt);
    }
}