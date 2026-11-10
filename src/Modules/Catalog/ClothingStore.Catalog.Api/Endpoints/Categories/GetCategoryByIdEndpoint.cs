namespace ClothingStore.Catalog.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/categories/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetCategoryByIdQuery(id), cancellationToken);

                return result.Match(Results.Ok, errors => errors.ToProblem());
            })
            .WithTags("Catalog - Categories")
            .WithSummary("Get category by id. Auth: Required | Role: Admin")
            .RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}
