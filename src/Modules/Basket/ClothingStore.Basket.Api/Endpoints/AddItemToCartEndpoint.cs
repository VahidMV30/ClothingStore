namespace ClothingStore.Basket.Api.Endpoints;

public class AddItemToCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/carts", async (AddItemToCartRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new AddItemToCartCommand(request.ProductId, request.Quantity);

            var result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, errors => errors.ToProblem());
        }).RequireAuthorization();
    }
}