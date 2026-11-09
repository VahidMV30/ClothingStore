namespace ClothingStore.Basket.Infrastructure.Persistence.Caching;

public class CartCache(IDistributedCache cache) : ICartCache
{
    public async Task InvalidateAsync(Guid userId, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(CartCacheKeys.For(userId), cancellationToken);
    }
}