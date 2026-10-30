namespace ClothingStore.Identity.Api.Endpoints;

public class LogoutEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/logout", async (ISender sender, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            httpContext.Request.Cookies.TryGetValue("refresh-token", out var refreshToken);

            await sender.Send(new LogoutCommand(refreshToken), cancellationToken);

            httpContext.Response.Cookies.Delete("refresh-token", new CookieOptions { Path = "/api/auth" });

            return Results.NoContent();
        });
    }
}