using AutoMapper;
using RestaurantService.API.Communication.Requests;
using RestaurantService.API.Communication.Responses;
using RestaurantService.API.Domain.Entities;

namespace RestaurantService.API.Communication.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantJsonResponse>();
        CreateMap<RestaurantJsonRequest, Restaurant>();
    }
}
