namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class ActivateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id:guid}/activate", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new ActivateProductCommand(id), cancellationToken);

            return result.Match(Results.Ok, errors => errors.ToProblem());
        }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}