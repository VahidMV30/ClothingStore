namespace ClothingStore.Ordering.Api.Endpoints;

public class GetMyOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/me", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetMyOrdersQuery(), cancellationToken);

                return Results.Ok(result);
            })
            .WithTags("Ordering")
            .WithSummary("Get current user orders. Auth: Required | Role: Any authenticated user")
            .RequireAuthorization();
    }
}