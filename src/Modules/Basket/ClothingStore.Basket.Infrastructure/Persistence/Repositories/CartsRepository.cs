namespace ClothingStore.Basket.Infrastructure.Persistence.Repositories;

public class CartsRepository(BasketDbContext dbContext) : BaseRepository<Guid, Cart>(dbContext), ICartsRepository
{
    public async Task<Cart?> GetByUserIdAsync(Guid userId, bool asNotTracking, CancellationToken cancellationToken)
    {
        var query = dbContext.Carts.AsQueryable();

        if (asNotTracking) query = query.AsNoTracking();

        var cart = await query
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return cart;
    }

    public async Task<List<Cart>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Carts
            .Include(x => x.Items)
            .Where(x => x.Items.Any(y => y.ProductId == productId))
            .ToListAsync(cancellationToken);
    }
}