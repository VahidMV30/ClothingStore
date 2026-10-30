namespace ClothingStore.Identity.Domain.Entities;

public class User : Aggregate<Guid>
{
    private readonly List<UserRole> _userRoles = [];

    private User()
    {
    }

    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
    public string FullName { get; private set; } = string.Empty;
    public MobileNumber Mobile { get; private set; } = null!;
    public string PasswordHash { get; private set; } = string.Empty;

    public static User Create(Guid id, string fullName, MobileNumber mobile, string passwordHash)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
        ArgumentNullException.ThrowIfNull(mobile);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        fullName = fullName.Trim();

        return new User
        {
            Id = id,
            FullName = fullName,
            Mobile = mobile,
            PasswordHash = passwordHash
        };
    }

    public void AssignRole(Guid userRoleId, Guid roleId)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(userRoleId, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(roleId, Guid.Empty);

        if (_userRoles.Any(x => x.RoleId == roleId)) return;

        _userRoles.Add(UserRole.Create(userRoleId, roleId, Id));
    }
}