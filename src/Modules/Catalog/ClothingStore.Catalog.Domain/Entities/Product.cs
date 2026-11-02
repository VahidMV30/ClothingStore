namespace ClothingStore.Catalog.Domain.Entities;

public class Product : Aggregate<Guid>
{
    private Product()
    {
    }

    public Guid CategoryId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int QuantityInStock { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    public static Product Create(Guid id, Guid categoryId, string name, decimal price, int quantityInStock, string imageUrl, string description)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(categoryId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0);
        ArgumentOutOfRangeException.ThrowIfNegative(quantityInStock);
        ArgumentException.ThrowIfNullOrWhiteSpace(imageUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        name = name.Trim();
        imageUrl = imageUrl.Trim();
        description = description.Trim();

        return new Product
        {
            Id = id,
            CategoryId = categoryId,
            Name = name,
            Price = price,
            QuantityInStock = quantityInStock,
            ImageUrl = imageUrl,
            Description = description
        };
    }

    public void Update(Guid categoryId, string name, decimal price, int quantityInStock, string imageUrl, string description)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(categoryId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0);
        ArgumentOutOfRangeException.ThrowIfNegative(quantityInStock);
        ArgumentException.ThrowIfNullOrWhiteSpace(imageUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        name = name.Trim();
        imageUrl = imageUrl.Trim();
        description = description.Trim();

        CategoryId = categoryId;
        Name = name;
        Price = price;
        QuantityInStock = quantityInStock;
        ImageUrl = imageUrl;
        Description = description;
    }

    public void IncreaseQuantityInStock(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);

        QuantityInStock += quantity;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}