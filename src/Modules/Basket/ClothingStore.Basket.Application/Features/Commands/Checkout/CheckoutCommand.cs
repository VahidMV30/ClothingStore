namespace ClothingStore.Basket.Application.Features.Commands.Checkout;

public record CheckoutCommand : ICommand<ErrorOr<Guid>>;