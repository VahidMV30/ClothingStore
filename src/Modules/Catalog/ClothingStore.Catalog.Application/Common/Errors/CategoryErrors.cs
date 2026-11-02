namespace ClothingStore.Catalog.Application.Common.Errors;

public static class CategoryErrors
{
    public static Error Duplicated => Error.Conflict("Categories.Duplicated", CategoryMessages.Duplicated);
    public static Error NotFound => Error.NotFound("Categories.NotFound", CategoryMessages.NotFound);
}