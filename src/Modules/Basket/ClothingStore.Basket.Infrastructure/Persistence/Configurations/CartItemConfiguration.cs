namespace ClothingStore.Basket.Infrastructure.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => new { x.CartId, x.ProductId }).IsUnique();

        builder.Property(x => x.ProductName).HasMaxLength(128).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(18, 0);
    }
}