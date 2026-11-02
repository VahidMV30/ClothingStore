using ClothingStore.Catalog.Application.Common.DTOs.Categories;

namespace ClothingStore.Catalog.Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name, string Description) : ICommand<ErrorOr<CategoryResponse>>;