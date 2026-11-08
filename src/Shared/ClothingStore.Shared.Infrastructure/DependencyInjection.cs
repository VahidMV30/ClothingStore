namespace ClothingStore.Shared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration,
        Assembly[] consumerAssemblies)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddMassTransit(busConfigurator =>
        {
            if (consumerAssemblies.Length > 0) busConfigurator.AddConsumers(consumerAssemblies);

            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQ:Host"] ?? "localhost", "/", host =>
                {
                    host.Username(configuration["RabbitMQ:UserName"] ?? "guest");
                    host.Password(configuration["RabbitMQ:Password"] ?? "guest");
                });

                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(2)));
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static async Task UseSharedInfrastructure(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        foreach (var moduleMigrator in serviceProvider.GetServices<IModuleMigrator>()) await moduleMigrator.MigrateAsync(CancellationToken.None);

        foreach (var dataSeeder in serviceProvider.GetServices<IDataSeeder>()) await dataSeeder.SeedAllAsync();
    }
}