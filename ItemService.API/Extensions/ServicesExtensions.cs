using ItemService.API.Application.UseCases;
using ItemService.API.Domain.IRepositories;
using ItemService.API.ExternalServices.RabbitMQ;
using ItemService.API.Infra.Repositories;

namespace ItemService.API.Extensions;

public static class ServicesExtensions
{
    public static void AddUseCaseServices(this IServiceCollection services)
    {
        services.AddScoped<RegisterItemUseCase>();
        services.AddScoped<RegisterRestaurantReferenceUseCase>();
    }
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IRestaurantReferenceRepository, RestaurantReferenceRepository>();
    }
    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMQSubscriber>();
    }
    public static void AddSingletonServices(this IServiceCollection services)
    {
        services.AddSingleton<ProcessQueueMessageUseCase>();
    }
}
