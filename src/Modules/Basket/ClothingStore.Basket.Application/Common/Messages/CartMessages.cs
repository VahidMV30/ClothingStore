namespace ClothingStore.Basket.Application.Common.Messages;

public static class CartMessages
{
    public const string ProductNotActive = "این محصول در حال حاضر قابل سفارش نیست.";
    public const string InsufficientStock = "موجودی انبار کافی نیست.";
    public const string CartNotFound = "سبد خرید یافت نشد.";
    public const string ProductNotInCart = "این محصول در سبد خرید وجود ندارد.";
    public const string CartIsEmpty = "سبد خرید شما خالی است.";

    public const string ProductNotFound = "محصول «{0}» یافت نشد.";
    public const string NotActive = "محصول «{0}» فعال نیست.";
    public const string InsufficientStockForProduct = "موجودی محصول «{0}» کافی نیست.";
    public const string PriceChanged = "قیمت محصول «{0}» تغییر کرده است. لطفاً سبد خرید را دوباره بررسی کنید.";
}