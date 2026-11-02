namespace ClothingStore.Catalog.Application.Features.Products.Commands.DeactivateProduct;

public record DeactivateProductCommand(Guid ProductId) : ICommand<ErrorOr<ProductResponse>>;