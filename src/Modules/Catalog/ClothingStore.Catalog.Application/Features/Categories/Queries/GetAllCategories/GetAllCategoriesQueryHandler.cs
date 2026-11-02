namespace ClothingStore.Catalog.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    ICategoriesRepository categoriesRepository) : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
{
    public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await categoriesRepository.GetAllCategoriesAsync(cancellationToken);
    }
}