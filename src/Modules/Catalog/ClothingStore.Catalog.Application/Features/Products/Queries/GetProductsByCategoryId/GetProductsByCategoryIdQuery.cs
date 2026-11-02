namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetProductsByCategoryId;

public record GetProductsByCategoryIdQuery(Guid CategoryId) : IQuery<IEnumerable<ProductResponse>>;