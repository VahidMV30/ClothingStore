namespace ClothingStore.Catalog.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryCommand, ErrorOr<CategoryResponse>>
{
    public async Task<ErrorOr<CategoryResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        command = command with { Name = command.Name.Trim(), Description = command.Description.Trim() };

        var category = await categoriesRepository.GetByIdAsync(command.Id, cancellationToken);

        if (category is null) return CategoryErrors.NotFound;

        if (await categoriesRepository.ExistsAsync(x => x.Name == command.Name && x.Id != command.Id, cancellationToken))
            return CategoryErrors.Duplicated;

        category.Update(command.Name, command.Description);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return category.Adapt<CategoryResponse>();
    }
}