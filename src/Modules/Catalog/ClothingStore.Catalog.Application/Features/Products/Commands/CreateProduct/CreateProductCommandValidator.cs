namespace ClothingStore.Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("لطفا شناسه دسته بندی را وارد نمایید.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("لطفا نام محصول را وارد نمایید.")
            .Length(3, 50).WithMessage("نام محصول باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("قیمت باید بیشتر از 0 باشد.");
        ;

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("موجودی انبار نمیتواند منفی باشد.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("لطفا آدرس عکس محصول را وارد نمایید.")
            .Length(3, 50).WithMessage("آدرس عکس محصول باید حداقل 3 و حداکثر 50 کاراکتر باشد.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("لطفا توضیحات را وارد نمایید.")
            .Length(10, 500).WithMessage("توضیحات باید حداقل 10 و حداکثر 500 کاراکتر باشد.");
    }
}