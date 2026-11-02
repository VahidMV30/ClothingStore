namespace ClothingStore.Catalog.Application.Common.DTOs.Products;

public record ProductResponse(
    Guid Id,
    Guid CategoryId,
    string Name,
    decimal Price,
    int QuantityInStock,
    string ImageUrl,
    string Description,
    bool IsActive);