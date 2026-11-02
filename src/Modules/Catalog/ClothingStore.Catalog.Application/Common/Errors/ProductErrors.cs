namespace ClothingStore.Catalog.Application.Common.Errors;

public static class ProductErrors
{
    public static Error Duplicated => Error.Conflict("Products.Duplicated", ProductMessages.Duplicated);
    public static Error NotFound => Error.NotFound("Products.NotFound", ProductMessages.NotFound);
}