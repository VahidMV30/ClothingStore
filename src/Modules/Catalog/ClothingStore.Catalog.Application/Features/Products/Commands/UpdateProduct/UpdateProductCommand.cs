namespace ClothingStore.Catalog.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, Guid CategoryId, string Name, decimal Price, int QuantityInStock, string ImageUrl, string Description)
    : ICommand<ErrorOr<ProductResponse>>;