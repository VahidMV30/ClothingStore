using ClothingStore.Catalog.Application.Common.DTOs.Categories;

namespace ClothingStore.Catalog.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, ErrorOr<CategoryResponse>>
{
    public async Task<ErrorOr<CategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        command = command with { Name = command.Name.Trim() };

        if (await categoriesRepository.ExistsAsync(x => x.Name == command.Name, cancellationToken))
            return CategoryErrors.Duplicated;

        var category = Category.Create(Guid.NewGuid(), command.Name, command.Description);

        await categoriesRepository.AddAsync(category, cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return category.Adapt<CategoryResponse>();
    }
}