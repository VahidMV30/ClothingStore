namespace ClothingStore.Basket.Domain.Entities;

public class Cart : Aggregate<Guid>
{
    private readonly List<CartItem> _items = [];

    private Cart()
    {
    }

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public Guid UserId { get; private set; }

    public decimal TotalPrice => _items.Sum(x => x.Quantity * x.UnitPrice);

    public static Cart Create(Guid id, Guid userId)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(userId, Guid.Empty);

        return new Cart
        {
            Id = id,
            UserId = userId
        };
    }

    public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem is null)
        {
            var item = CartItem.Create(Guid.NewGuid(), Id, productId, productName, unitPrice, quantity);

            _items.Add(item);
        }
        else
        {
            existingItem.IncreaseQuantity(quantity);
        }
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null) return;

        _items.Remove(item);
    }

    public void UpdateCartItemQuantity(Guid productId, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(productId, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);

        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        item?.UpdateQuantity(quantity);
    }

    public void ClearCartItems()
    {
        _items.Clear();
    }

    public void UpdateCartItemPrice(Guid productId, decimal newPrice)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(productId, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(newPrice, 0);

        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        item?.UpdatePrice(newPrice);
    }
}