namespace ClothingStore.Catalog.Api.Endpoints.Products;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id), cancellationToken);

            return result.Match(Results.Ok, errors => errors.ToProblem());
        });
    }
}