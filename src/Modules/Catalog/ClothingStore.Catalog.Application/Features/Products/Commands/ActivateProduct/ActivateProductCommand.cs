namespace ClothingStore.Catalog.Application.Features.Products.Commands.ActivateProduct;

public record ActivateProductCommand(Guid ProductId) : ICommand<ErrorOr<ProductResponse>>;