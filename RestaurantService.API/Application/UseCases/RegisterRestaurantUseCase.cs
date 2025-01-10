using AutoMapper;
using RestaurantService.API.Communication.Requests;
using RestaurantService.API.Communication.Responses;
using RestaurantService.API.Domain.Entities;
using RestaurantService.API.Domain.Repositories;

namespace RestaurantService.API.Application.UseCases;

public class RegisterRestaurantUseCase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RegisterRestaurantUseCase(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    public async Task<RestaurantJsonResponse> Execute(RestaurantJsonRequest request)
    {
        var restaurant = await _restaurantRepository.Create(_mapper.Map<Restaurant>(request));
        await _restaurantRepository.SaveChanges();
        return _mapper.Map<RestaurantJsonResponse>(restaurant);
    }
}
