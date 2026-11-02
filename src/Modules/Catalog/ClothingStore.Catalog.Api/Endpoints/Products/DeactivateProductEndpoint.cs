namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class DeactivateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id:guid}/Deactivate", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new DeactivateProductCommand(id), cancellationToken);

            return result.Match(Results.Ok, errors => errors.ToProblem());
        }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}