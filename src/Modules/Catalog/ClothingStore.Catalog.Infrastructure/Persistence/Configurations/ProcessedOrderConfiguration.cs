namespace ClothingStore.Catalog.Infrastructure.Persistence.Configurations;

public class ProcessedOrderConfiguration : IEntityTypeConfiguration<ProcessedOrder>
{
    public void Configure(EntityTypeBuilder<ProcessedOrder> builder)
    {
        builder.HasKey(x => x.OrderId);
        builder.Property(x => x.OrderId).ValueGeneratedNever();
    }
}