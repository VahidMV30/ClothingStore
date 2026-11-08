namespace ClothingStore.Ordering.Api;

public static class OrderingModule
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOrderingInfrastructure(configuration);

        return services;
    }
}