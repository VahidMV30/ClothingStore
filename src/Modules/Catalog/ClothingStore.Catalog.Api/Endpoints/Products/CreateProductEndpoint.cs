namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (CreateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateProductCommand(
                request.CategoryId,
                request.Name,
                request.Price,
                request.QuantityInStock,
                request.ImageUrl,
                request.Description);

            var result = await sender.Send(command, cancellationToken);

            return result.Match(
                value => Results.Created($"/api/products/{value.Id}", value),
                errors => errors.ToProblem());
        }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
        ;
    }
}