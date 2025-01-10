using RestaurantService.API.Application.UseCases;
using RestaurantService.API.Domain.Repositories;
using RestaurantService.API.Infra.Repositories;

namespace RestaurantService.API.Extensions;

public static class ServicesExtentions
{
    public static void AddUseCaseServices(this IServiceCollection services)
    {
        services.AddScoped<RegisterRestaurantUseCase>();

    }
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    }
}
