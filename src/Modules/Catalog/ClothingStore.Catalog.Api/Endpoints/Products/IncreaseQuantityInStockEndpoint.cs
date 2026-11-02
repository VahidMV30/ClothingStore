namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class IncreaseQuantityInStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id:guid}/Increase",
            async (Guid id, IncreaseQuantityInStockRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new IncreaseQuantityInStockCommand(id, request.Quantity), cancellationToken);

                return result.Match(Results.Ok, errors => errors.ToProblem());
            }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}