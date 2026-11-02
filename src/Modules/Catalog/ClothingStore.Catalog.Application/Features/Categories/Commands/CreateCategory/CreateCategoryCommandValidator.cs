namespace ClothingStore.Catalog.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("لطفا نام دسته بندی را وارد نمایید.")
            .Length(3, 50).WithMessage("نام دسته بندی باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("لطفا توضیحات را وارد نمایید.")
            .Length(10, 500).WithMessage("توضیحات باید حداقل 10 و حداکثر 500 کاراکتر باشد.");
    }
}