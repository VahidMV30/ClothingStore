namespace ClothingStore.Ordering.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ProductName).HasMaxLength(128).IsRequired();
        builder.Property(x => x.UnitPrice).HasPrecision(18, 0);
    }
}