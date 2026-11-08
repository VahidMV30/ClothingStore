namespace ClothingStore.Catalog.Domain.Entities;

public class ProcessedOrder
{
    private ProcessedOrder()
    {
    }

    public Guid OrderId { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    public static ProcessedOrder Create(Guid orderId, DateTime now)
    {
        return new ProcessedOrder { OrderId = orderId, ProcessedAt = now };
    }
}