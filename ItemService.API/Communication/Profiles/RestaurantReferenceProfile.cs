using AutoMapper;
using ItemService.API.Communication.Requests;
using ItemService.API.Communication.Responses;
using ItemService.API.Domain.Entities;

namespace ItemService.API.Communication.Profiles;

public class RestaurantReferenceProfile : Profile
{
    public RestaurantReferenceProfile()
    {
        CreateMap<RestaurantReferenceJsonRequest, RestaurantReference>();
        CreateMap<RestaurantReference, RestaurantReferenceJsonResponse>();
    }
}
