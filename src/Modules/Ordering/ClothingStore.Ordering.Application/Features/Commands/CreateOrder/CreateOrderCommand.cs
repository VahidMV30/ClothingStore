namespace ClothingStore.Ordering.Application.Features.Commands.CreateOrder;

public record CreateOrderCommand(Guid OrderId, Guid UserId, List<CreateOrderItems> Items) : ICommand<Unit>;

public record CreateOrderItems(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);