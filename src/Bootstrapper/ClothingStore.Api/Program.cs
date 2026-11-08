using BasketApplication = ClothingStore.Basket.Application;
using BasketInfrastructure = ClothingStore.Basket.Infrastructure;
using CatalogApplication = ClothingStore.Catalog.Application;
using CatalogInfrastructure = ClothingStore.Catalog.Infrastructure;
using IdentityApplication = ClothingStore.Identity.Application;
using OrderingApplication = ClothingStore.Ordering.Application;
using OrderingInfrastructure = ClothingStore.Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Application Assemblies
var identityApplicationAssembly = typeof(IdentityApplication.DependencyInjection).Assembly;
var catalogApplicationAssembly = typeof(CatalogApplication.DependencyInjection).Assembly;
var basketApplicationAssembly = typeof(BasketApplication.DependencyInjection).Assembly;
var orderingApplicationAssembly = typeof(OrderingApplication.DependencyInjection).Assembly;

var applicationAssemblies = new List<Assembly>
{
    identityApplicationAssembly,
    catalogApplicationAssembly,
    basketApplicationAssembly,
    orderingApplicationAssembly
};

// Api Assemblies
var identityApiAssembly = typeof(IdentityModule).Assembly;
var catalogApiAssembly = typeof(CatalogModule).Assembly;
var basketApiAssembly = typeof(BasketModule).Assembly;
var orderingApiAssembly = typeof(OrderingModule).Assembly;

var apiAssemblies = new List<Assembly>
{
    identityApiAssembly,
    catalogApiAssembly,
    basketApiAssembly,
    orderingApiAssembly
};

// Infrastructure Assemblies
var catalogInfrastructureAssembly = typeof(CatalogInfrastructure.DependencyInjection).Assembly;
var basketInfrastructureAssembly = typeof(BasketInfrastructure.DependencyInjection).Assembly;
var orderingInfrastructureAssembly = typeof(OrderingInfrastructure.DependencyInjection).Assembly;

var infrastructureAssemblies = new List<Assembly>
{
    catalogInfrastructureAssembly,
    basketInfrastructureAssembly,
    orderingInfrastructureAssembly
};

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);
builder.Services.AddSharedModule(builder.Configuration,
    [.. applicationAssemblies], [.. apiAssemblies], [.. infrastructureAssemblies]);

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