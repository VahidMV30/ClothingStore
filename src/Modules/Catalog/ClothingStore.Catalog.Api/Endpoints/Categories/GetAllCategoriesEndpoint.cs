namespace ClothingStore.Catalog.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/categories", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetAllCategoriesQuery(), cancellationToken);

                return Results.Ok(result);
            })
            .WithTags("Catalog - Categories")
            .WithSummary("Get all categories. Auth: Not required");
    }
}