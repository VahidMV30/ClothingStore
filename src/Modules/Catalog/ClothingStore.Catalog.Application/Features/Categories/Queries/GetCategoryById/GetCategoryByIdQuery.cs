namespace ClothingStore.Catalog.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<ErrorOr<CategoryResponse>>;