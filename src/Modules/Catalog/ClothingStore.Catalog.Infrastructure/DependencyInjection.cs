namespace ClothingStore.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("ClothingStore"),
                sql => { sql.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog"); });
        });

        services.AddScoped<IModuleMigrator, ModuleMigrator<CatalogDbContext>>();
        services.AddScoped<IDataSeeder, CatalogDataSeeder>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CatalogDbContext>());

        services.AddSingleton(TimeProvider.System);

        services.AddHostedService<OutboxProcessor<CatalogDbContext>>();

        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IOutboxWriter, OutboxWriter>();
        services.AddScoped<IProcessedOrdersRepository, ProcessedOrdersRepository>();

        return services;
    }
}