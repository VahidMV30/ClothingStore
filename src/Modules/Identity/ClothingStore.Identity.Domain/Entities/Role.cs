namespace ClothingStore.Identity.Domain.Entities;

public class Role : Aggregate<Guid>
{
    private Role()
    {
    }

    public string Name { get; private set; } = string.Empty;

    public static Role Create(Guid id, string name)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        name = name.Trim();

        return new Role { Id = id, Name = name };
    }
}