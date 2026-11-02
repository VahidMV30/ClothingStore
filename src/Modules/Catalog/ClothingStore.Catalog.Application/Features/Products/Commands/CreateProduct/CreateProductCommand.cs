namespace ClothingStore.Catalog.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(Guid CategoryId, string Name, decimal Price, int QuantityInStock, string ImageUrl, string Description)
    : ICommand<ErrorOr<ProductResponse>>;