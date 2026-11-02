namespace ClothingStore.Catalog.Application.Features.Products.Commands.IncreaseQuantityInStock;

public class IncreaseQuantityInStockCommandValidator : AbstractValidator<IncreaseQuantityInStockCommand>
{
    public IncreaseQuantityInStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("لطفا شناسه محصول را وارد نمایید.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("موجودی انبار باید بیشتر از 0 باشد.");
    }
}