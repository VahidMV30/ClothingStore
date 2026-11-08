namespace ClothingStore.Basket.Api;

public static class BasketModule
{
    public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBasketInfrastructure(configuration);

        return services;
    }
}