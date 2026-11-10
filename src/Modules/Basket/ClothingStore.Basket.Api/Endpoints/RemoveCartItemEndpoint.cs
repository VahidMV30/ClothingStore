namespace ClothingStore.Basket.Api.Endpoints;

public class RemoveCartItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/carts/items/{productId:guid}", async (Guid productId, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new RemoveCartItemCommand(productId), cancellationToken);

                return Results.Ok(result);
            })
            .WithTags("Basket")
            .WithSummary("Remove an item from cart. Auth: Required | Role: Any authenticated user")
            .RequireAuthorization();
    }
}