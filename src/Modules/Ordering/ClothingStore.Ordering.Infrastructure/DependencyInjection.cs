namespace ClothingStore.Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<OrderingDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(configuration.GetConnectionString("ClothingStore"),
                sql => { sql.MigrationsHistoryTable("__EFMigrationsHistory", "Ordering"); });
        });

        services.AddScoped<IModuleMigrator, ModuleMigrator<OrderingDbContext>>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<OrderingDbContext>());

        services.AddSingleton(TimeProvider.System);

        services.AddHostedService<OutboxProcessor<OrderingDbContext>>();

        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOutboxWriter, OutboxWriter>();

        return services;
    }
}