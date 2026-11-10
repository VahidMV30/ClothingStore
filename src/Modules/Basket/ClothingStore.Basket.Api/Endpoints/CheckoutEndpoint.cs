namespace ClothingStore.Basket.Api.Endpoints;

public class CheckoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/carts/checkout", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new CheckoutCommand(), cancellationToken);

                return result.Match(Results.Ok, errors => errors.ToProblem());
            })
            .WithTags("Basket")
            .WithSummary("Checkout the cart. Auth: Required | Role: Any authenticated user")
            .RequireAuthorization();
    }
}