namespace ClothingStore.Catalog.Application.Features.Products.Commands.DecreaseStockForOrder;

public record DecreaseStockForOrderCommand(Guid OrderId, List<DecreaseStockItems> Items) : ICommand<Unit>;

public record DecreaseStockItems(Guid ProductId, int Quantity);