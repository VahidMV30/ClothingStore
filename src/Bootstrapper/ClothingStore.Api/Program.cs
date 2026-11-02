using CatalogApplication = ClothingStore.Catalog.Application;
using IdentityApplication = ClothingStore.Identity.Application;

var builder = WebApplication.CreateBuilder(args);

// Application Assemblies
var identityApplicationAssembly = typeof(IdentityApplication.DependencyInjection).Assembly;
var catalogApplicationAssembly = typeof(CatalogApplication.DependencyInjection).Assembly;

var applicationAssemblies = new List<Assembly>
{
    identityApplicationAssembly,
    catalogApplicationAssembly
};

// Api Assemblies
var identityApiAssembly = typeof(IdentityModule).Assembly;
var catalogApiAssembly = typeof(CatalogModule).Assembly;

var apiAssemblies = new List<Assembly>
{
    identityApiAssembly,
    catalogApiAssembly
};

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddSharedModule([.. applicationAssemblies], [.. apiAssemblies]);

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseSharedModule();

app.Run();