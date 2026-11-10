namespace ClothingStore.Catalog.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/categories/{id:guid}", async (Guid id, UpdateCategoryRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new UpdateCategoryCommand(id, request.Name, request.Description);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, errors => errors.ToProblem());
            })
            .WithTags("Catalog - Categories")
            .WithSummary("Update existing category. Auth: Required | Role: Admin")
            .RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}