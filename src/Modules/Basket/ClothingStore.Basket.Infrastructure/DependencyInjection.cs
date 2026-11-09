namespace ClothingStore.Basket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBasketInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<BasketDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetRequiredService<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("ClothingStore"),
                sql => { sql.MigrationsHistoryTable("__EFMigrationsHistory", "Basket"); });
        });

        services.AddScoped<IModuleMigrator, ModuleMigrator<BasketDbContext>>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<BasketDbContext>());

        services.AddSingleton(TimeProvider.System);

        services.AddStackExchangeRedisCache(options => { options.Configuration = configuration.GetConnectionString("Redis") ?? "localhost:6379"; });

        services.AddHostedService<OutboxProcessor<BasketDbContext>>();

        services.AddScoped<ICartsRepository, CartsRepository>();
        services.Decorate<ICartsRepository, CachedCartRepository>();
        services.AddScoped<ICartCache, CartCache>();

        services.AddScoped<IOutboxWriter, OutboxWriter>();

        return services;
    }
}