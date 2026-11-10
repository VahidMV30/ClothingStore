namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class GetProductsByCategoryIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/category/{categoryId:guid}", async (Guid categoryId, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetProductsByCategoryIdQuery(categoryId), cancellationToken);

                return Results.Ok(result);
            })
            .WithTags("Catalog - Products")
            .WithSummary("Get product by category id. Auth: Not required");
    }
}