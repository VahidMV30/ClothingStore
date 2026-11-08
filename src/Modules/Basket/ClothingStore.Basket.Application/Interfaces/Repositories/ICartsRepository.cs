namespace ClothingStore.Basket.Application.Interfaces.Repositories;

public interface ICartsRepository : IBaseRepository<Guid, Cart>
{
    Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Cart>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
}