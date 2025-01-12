using AutoMapper;
using ItemService.API.Communication.Requests;
using ItemService.API.Domain.Entities;
using ItemService.API.Domain.IRepositories;
using System.Text.Json;

namespace ItemService.API.Application.UseCases;

public class ProcessQueueMessageUseCase 
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public ProcessQueueMessageUseCase(IMapper mapper, IServiceScopeFactory scopeFactory)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
    }

    public async Task Execute(string message)
    {
        using var scope = _scopeFactory.CreateScope();
        var restaurantReferenceRepository = scope.ServiceProvider.GetService<IRestaurantReferenceRepository>();
        var restaurantReferenceRequest = JsonSerializer.Deserialize<RestaurantReferenceJsonRequest>(message);
        var entity = _mapper.Map<RestaurantReference>(restaurantReferenceRequest);
        var restaurantAlreadyExists = await restaurantReferenceRepository!.GetById(entity.Id);
        if (restaurantAlreadyExists is null) {
            await restaurantReferenceRepository.Create(entity);
            await restaurantReferenceRepository.SaveChanges();
        }
    }

}
