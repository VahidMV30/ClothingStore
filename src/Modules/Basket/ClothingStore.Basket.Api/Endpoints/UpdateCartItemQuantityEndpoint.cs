namespace ClothingStore.Basket.Api.Endpoints;

public class UpdateCartItemQuantityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/carts/items/{productId:guid}/update-quantity",
                async (Guid productId, UpdateCartItemQuantityRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new UpdateCartItemQuantityCommand(productId, request.Quantity);

                    var result = await sender.Send(command, cancellationToken);

                    return result.Match(Results.Ok, errors => errors.ToProblem());
                })
            .WithTags("Basket")
            .WithSummary("Update existing cart item. Auth: Required | Role: Any authenticated user")
            .RequireAuthorization();
    }
}