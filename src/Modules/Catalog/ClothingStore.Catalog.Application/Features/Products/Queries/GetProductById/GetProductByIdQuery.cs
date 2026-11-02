namespace ClothingStore.Catalog.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ErrorOr<ProductResponse>>;