namespace ClothingStore.Basket.Application.Interfaces.Caching;

public interface ICartCache
{
    Task InvalidateAsync(Guid userId, CancellationToken cancellationToken);
}