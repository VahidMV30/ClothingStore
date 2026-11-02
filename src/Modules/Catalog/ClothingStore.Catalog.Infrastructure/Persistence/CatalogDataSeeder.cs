using System.Text.Json;

namespace ClothingStore.Catalog.Infrastructure.Persistence;

public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task SeedAllAsync()
    {
        if (!await dbContext.Categories.AnyAsync())
        {
            var items = await ReadAsync<CategorySeed>("categories.json");
            var categories = items.Select(x => Category.Create(x.Id, x.Name, x.Description)).ToList();

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Products.AnyAsync())
        {
            var items = await ReadAsync<ProductSeed>("products.json");
            var products = items.Select(x => Product.Create(
                x.Id,
                x.CategoryId,
                x.Name,
                x.Price,
                x.QuantityInStock,
                x.ImageUrl,
                x.Description)).ToList();

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.CommitChangesAsync(CancellationToken.None);
        }
    }

    private static async Task<List<T>> ReadAsync<T>(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Persistence", "SeedData", fileName);

        await using var stream = File.OpenRead(path);

        return await JsonSerializer.DeserializeAsync<List<T>>(stream, JsonOptions) ?? [];
    }

    private record CategorySeed(Guid Id, string Name, string Description);

    private record ProductSeed(Guid Id, Guid CategoryId, string Name, decimal Price, int QuantityInStock, string ImageUrl, string Description);
}