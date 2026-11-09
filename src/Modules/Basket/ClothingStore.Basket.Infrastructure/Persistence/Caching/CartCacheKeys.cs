namespace ClothingStore.Basket.Infrastructure.Persistence.Caching;

public static class CartCacheKeys
{
    public static string For(Guid userId)
    {
        return $"cart:{userId}";
    }
}