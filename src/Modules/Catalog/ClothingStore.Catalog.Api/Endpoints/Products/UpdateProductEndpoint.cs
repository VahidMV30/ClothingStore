namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id:guid}",
            async (Guid id, UpdateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new UpdateProductCommand(id,
                    request.CategoryId,
                    request.Name,
                    request.Price,
                    request.QuantityInStock,
                    request.ImageUrl,
                    request.Description);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, errors => errors.ToProblem());
            }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}