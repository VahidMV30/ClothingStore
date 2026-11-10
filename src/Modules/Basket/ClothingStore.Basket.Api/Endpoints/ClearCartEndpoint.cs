namespace ClothingStore.Basket.Api.Endpoints;

public class ClearCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/carts/items/clear", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new ClearCartCommand(), cancellationToken);

                return Results.Ok(result);
            })
            .WithTags("Basket")
            .WithSummary("Clear cart. Auth: Required | Role: Any authenticated user")
            .RequireAuthorization();
    }
}