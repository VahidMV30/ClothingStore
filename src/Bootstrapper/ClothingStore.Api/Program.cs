using IdentityApplication = ClothingStore.Identity.Application;

var builder = WebApplication.CreateBuilder(args);

// Application Assemblies
var identityApplicationAssembly = typeof(IdentityApplication.DependencyInjection).Assembly;

// Api Assemblies
var identityApiAssembly = typeof(IdentityModule).Assembly;

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddSharedModule([identityApplicationAssembly], [identityApiAssembly]);

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