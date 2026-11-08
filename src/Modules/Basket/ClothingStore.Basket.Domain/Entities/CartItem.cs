namespace ClothingStore.Basket.Domain.Entities;

public class CartItem : Entity<Guid>
{
    private CartItem()
    {
    }

    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    internal static CartItem Create(Guid id, Guid cartId, Guid productId, string productName, decimal unitPrice, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(cartId, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(productId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(unitPrice, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);

        productName = productName.Trim();

        return new CartItem
        {
            Id = id,
            CartId = cartId,
            ProductId = productId,
            ProductName = productName,
            UnitPrice = unitPrice,
            Quantity = quantity
        };
    }

    internal void IncreaseQuantity(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);

        Quantity += quantity;
    }

    internal void UpdateQuantity(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);

        Quantity = quantity;
    }

    internal void UpdatePrice(decimal newPrice)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(newPrice, 0);

        UnitPrice = newPrice;
    }
}