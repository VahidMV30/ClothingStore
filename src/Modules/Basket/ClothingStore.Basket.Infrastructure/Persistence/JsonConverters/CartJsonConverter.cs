namespace ClothingStore.Basket.Infrastructure.Persistence.JsonConverters;

public class CartJsonConverter : JsonConverter<Cart>
{
    public override Cart? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var snapshot = JsonSerializer.Deserialize<CartSnapshot>(ref reader, options)!;

        var cart = Cart.Create(snapshot.Id, snapshot.UserId);

        foreach (var item in snapshot.Items) cart.RestoreItem(item.Id, item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);

        return cart;
    }

    public override void Write(Utf8JsonWriter writer, Cart value, JsonSerializerOptions options)
    {
        var snapshot = new CartSnapshot(value.Id, value.UserId, [
            .. value.Items
                .Select(x => new CartItemSnapshot(x.Id, x.ProductId, x.ProductName, x.UnitPrice, x.Quantity))
        ]);

        JsonSerializer.Serialize(writer, snapshot, options);
    }

    private record CartSnapshot(Guid Id, Guid UserId, List<CartItemSnapshot> Items);

    private record CartItemSnapshot(Guid Id, Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);
}