using AutoMapper;
using ItemService.API.Communication.Requests;
using ItemService.API.Communication.Responses;
using ItemService.API.Domain.Entities;
using ItemService.API.Domain.IRepositories;

namespace ItemService.API.Application.UseCases;

public class RegisterRestaurantReferenceUseCase
{
    private readonly IRestaurantReferenceRepository _restaurantReferenceRepository;
    private readonly IMapper _mapper;

    public RegisterRestaurantReferenceUseCase(IRestaurantReferenceRepository repository, IMapper mapper)
    {
        _restaurantReferenceRepository = repository;
        _mapper = mapper;
    }

    public async Task<RestaurantReferenceJsonResponse> Execute(RestaurantReferenceJsonRequest request)
    {
        var entity = await _restaurantReferenceRepository.Create(_mapper.Map<RestaurantReference>(request));
        await _restaurantReferenceRepository.SaveChanges();
        return _mapper.Map<RestaurantReferenceJsonResponse>(entity);
    }
}
