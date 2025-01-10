using ItemService.API.Application.UseCases;
using ItemService.API.Domain.IRepositories;
using ItemService.API.Infra.Repositories;

namespace ItemService.API.Extensions;

public static class ServicesExtensions
{
    public static void AddUseCaseServices(this IServiceCollection services)
    {
        services.AddScoped<RegisterItemUseCase>();
    }
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
    }
}
