namespace ClothingStore.Catalog.Contracts.Features.Products.Queries.GetProductSummaryById;

public record GetProductSummaryByIdQuery(Guid ProductId) : IQuery<ErrorOr<ProductSummaryResponse>>;