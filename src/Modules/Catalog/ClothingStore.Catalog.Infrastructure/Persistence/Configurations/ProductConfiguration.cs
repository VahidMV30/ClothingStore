namespace ClothingStore.Catalog.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Price).HasPrecision(18, 0);
        builder.Property(x => x.ImageUrl).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1024).IsRequired();

        builder.Ignore(x => x.DomainEvents);

        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}