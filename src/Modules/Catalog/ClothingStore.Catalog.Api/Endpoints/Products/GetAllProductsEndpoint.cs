namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetAllProductsQuery(), cancellationToken);

            return Results.Ok(result);
        });
    }
}