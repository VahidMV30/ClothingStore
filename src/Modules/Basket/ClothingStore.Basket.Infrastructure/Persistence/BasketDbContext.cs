namespace ClothingStore.Basket.Infrastructure.Persistence;

public class BasketDbContext(DbContextOptions<BasketDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public async Task CommitChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(CancellationToken.None);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("Basket");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}