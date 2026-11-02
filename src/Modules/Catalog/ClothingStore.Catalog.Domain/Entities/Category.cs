namespace ClothingStore.Catalog.Domain.Entities;

public class Category : Aggregate<Guid>
{
    private Category()
    {
    }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Category Create(Guid id, string name, string description)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        name = name.Trim();
        description = description.Trim();

        return new Category
        {
            Id = id,
            Name = name,
            Description = description
        };
    }

    public void Update(string name, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        Name = name.Trim();
        Description = description.Trim();
    }
}