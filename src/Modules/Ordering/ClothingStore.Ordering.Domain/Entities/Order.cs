namespace ClothingStore.Ordering.Domain.Entities;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _items = [];

    private Order()
    {
    }

    public Guid UserId { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => _items.Sum(x => x.Quantity * x.UnitPrice);

    public static Order Create(Guid id, Guid userId, IReadOnlyCollection<OrderItemData> items)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(userId, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfZero(items.Count);

        var order = new Order { Id = id, UserId = userId };

        foreach (var item in items) order.AddItem(item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);

        order.AddDomainEvent(new OrderCreatedEvent(order.Id, order.UserId,
            [.. order.Items.Select(x => new OrderCreatedItem(x.ProductId, x.Quantity))]));

        return order;
    }

    private void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        _items.Add(OrderItem.Create(Guid.NewGuid(), Id, productId, productName, unitPrice, quantity));
    }
}