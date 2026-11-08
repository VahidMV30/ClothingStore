namespace ClothingStore.Catalog.Contracts.Common.DTOs;

public record ProductSummaryResponse(Guid ProductId, string Name, decimal Price, int QuantityInStock, bool IsActive);