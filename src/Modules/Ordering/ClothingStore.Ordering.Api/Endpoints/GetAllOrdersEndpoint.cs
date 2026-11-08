namespace ClothingStore.Ordering.Api.Endpoints;

public class GetAllOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetAllOrdersQuery(), cancellationToken);

            return Results.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}