namespace ClothingStore.Basket.Application.Common.Errors;

public static class CartErrors
{
    public static Error ProductNotActive => Error.Validation("Carts.ProductNotActive", CartMessages.ProductNotActive);
    public static Error InsufficientStock => Error.Validation("Carts.InsufficientStock", CartMessages.InsufficientStock);
    public static Error CartNotFound => Error.NotFound("Carts.CartNotFound", CartMessages.CartNotFound);
    public static Error ProductNotInCart => Error.NotFound("Carts.ProductNotInCart", CartMessages.ProductNotInCart);
    public static Error CartIsEmpty => Error.Validation("Carts.CartIsEmpty", CartMessages.CartIsEmpty);

    public static Error ProductNotFound(string productName)
    {
        return Error.NotFound("Carts.ProductNotFound", string.Format(CartMessages.ProductNotFound, productName));
    }

    public static Error NotActive(string productName)
    {
        return Error.Validation("Carts.NotActive", string.Format(CartMessages.NotActive, productName));
    }

    public static Error InsufficientStockForProduct(string productName)
    {
        return Error.Validation("Carts.InsufficientStockForProduct", string.Format(CartMessages.InsufficientStockForProduct, productName));
    }

    public static Error PriceChanged(string productName)
    {
        return Error.Validation("Carts.PriceChanged", string.Format(CartMessages.PriceChanged, productName));
    }
}