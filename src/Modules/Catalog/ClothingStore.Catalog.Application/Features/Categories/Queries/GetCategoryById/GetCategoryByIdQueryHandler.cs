namespace ClothingStore.Catalog.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(ICategoriesRepository categoriesRepository) : IQueryHandler<GetCategoryByIdQuery, ErrorOr<CategoryResponse>>
{
    public async Task<ErrorOr<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetCategoryByIdAsync(query.Id, cancellationToken);

        if (category is null) return CategoryErrors.NotFound;

        return category;
    }
}