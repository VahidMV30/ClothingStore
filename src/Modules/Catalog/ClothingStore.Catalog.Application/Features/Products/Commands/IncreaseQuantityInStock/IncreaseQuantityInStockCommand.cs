namespace ClothingStore.Catalog.Application.Features.Products.Commands.IncreaseQuantityInStock;

public record IncreaseQuantityInStockCommand(Guid ProductId, int Quantity) : ICommand<ErrorOr<ProductResponse>>;