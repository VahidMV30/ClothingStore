namespace ClothingStore.Basket.Infrastructure.Persistence.Caching;

public class CachedCartRepository(ICartsRepository inner, IDistributedCache cache) : ICartsRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Converters = { new CartJsonConverter() }
    };

    public Task<IEnumerable<Cart>> GetAllAsync(CancellationToken cancellationToken)
    {
        return inner.GetAllAsync(cancellationToken);
    }

    public Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return inner.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> ExistsAsync(Expression<Func<Cart, bool>> predicate, CancellationToken cancellationToken)
    {
        return inner.ExistsAsync(predicate, cancellationToken);
    }

    public async Task AddAsync(Cart entity, CancellationToken cancellationToken)
    {
        await inner.AddAsync(entity, cancellationToken);
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId, bool asNoTracking, CancellationToken cancellationToken)
    {
        if (!asNoTracking) return await inner.GetByUserIdAsync(userId, false, cancellationToken);

        var key = CartCacheKeys.For(userId);

        var cached = await cache.GetStringAsync(key, cancellationToken);
        if (cached is not null) return JsonSerializer.Deserialize<Cart>(cached, JsonOptions);

        var cart = await inner.GetByUserIdAsync(userId, true, cancellationToken);

        if (cart is not null)
            await cache.SetStringAsync(key, JsonSerializer.Serialize(cart, JsonOptions),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) }, cancellationToken);

        return cart;
    }

    public Task<List<Cart>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return inner.GetByProductIdAsync(productId, cancellationToken);
    }
}