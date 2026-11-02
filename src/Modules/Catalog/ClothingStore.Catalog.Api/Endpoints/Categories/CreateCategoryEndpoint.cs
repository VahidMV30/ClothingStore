using ClothingStore.Catalog.Application.Common.DTOs.Categories;

namespace ClothingStore.Catalog.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async (CreateCategoryRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateCategoryCommand(request.Name, request.Description);

            var result = await sender.Send(command, cancellationToken);

            return result.Match(
                value => Results.Created($"/api/categories/{value.Id}", value),
                errors => errors.ToProblem());
        }).RequireAuthorization(policy => policy.RequireRole(ApplicationRoles.Admin));
    }
}