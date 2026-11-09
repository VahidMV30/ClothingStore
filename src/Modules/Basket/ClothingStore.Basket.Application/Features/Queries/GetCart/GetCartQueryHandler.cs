namespace ClothingStore.Basket.Application.Features.Queries.GetCart;

public class GetCartQueryHandler(
    ICurrentUser currentUser,
    ICartsRepository cartsRepository) : IQueryHandler<GetCartQuery, CartResponse>
{
    public async Task<CartResponse> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await cartsRepository.GetByUserIdAsync(currentUser.Id, true, cancellationToken);

        return cart is null ? new CartResponse(Guid.Empty, [], 0) : cart.Adapt<CartResponse>();
    }
}