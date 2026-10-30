namespace ClothingStore.Identity.Domain.Entities;

public class UserRole : Entity<Guid>
{
    private UserRole()
    {
    }

    public Guid RoleId { get; private set; }
    public Guid UserId { get; private set; }

    internal static UserRole Create(Guid id, Guid roleId, Guid userId)
    {
        return new UserRole { Id = id, RoleId = roleId, UserId = userId };
    }
}