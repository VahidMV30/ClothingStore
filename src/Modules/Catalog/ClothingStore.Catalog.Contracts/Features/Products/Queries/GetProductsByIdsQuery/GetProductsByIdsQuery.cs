namespace ClothingStore.Catalog.Contracts.Features.Products.Queries.GetProductsByIdsQuery;

public record GetProductsByIdsQuery(List<Guid> ProductIds) : IQuery<List<ProductSummaryResponse>>;