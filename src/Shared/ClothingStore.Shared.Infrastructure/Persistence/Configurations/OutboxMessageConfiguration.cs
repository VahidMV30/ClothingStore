namespace ClothingStore.Shared.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => new { x.ProcessedAt, x.CreatedAt });

        builder.Property(x => x.Type).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Error).HasMaxLength(2048);
    }
}