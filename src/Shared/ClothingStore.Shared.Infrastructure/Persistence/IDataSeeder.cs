namespace ClothingStore.Shared.Infrastructure.Persistence;

public interface IDataSeeder
{
    Task SeedAllAsync();
}