namespace ClothingStore.Basket.Api.Endpoints;

public class GetCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/carts", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCartQuery(), cancellationToken);

            return Results.Ok(result);
        }).RequireAuthorization();
    }
}