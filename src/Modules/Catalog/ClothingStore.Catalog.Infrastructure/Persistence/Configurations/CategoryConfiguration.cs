namespace ClothingStore.Catalog.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1024).IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}