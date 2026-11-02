using ClothingStore.Catalog.Application.Common.DTOs.Categories;

namespace ClothingStore.Catalog.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Description) : ICommand<ErrorOr<CategoryResponse>>;