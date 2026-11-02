namespace ClothingStore.Catalog.Application.Common.DTOs.Products;

public record CreateProductRequest(Guid CategoryId, string Name, decimal Price, int QuantityInStock, string ImageUrl, string Description);