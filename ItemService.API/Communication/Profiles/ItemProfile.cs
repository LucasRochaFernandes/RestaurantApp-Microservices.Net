using AutoMapper;
using ItemService.API.Communication.Requests;
using ItemService.API.Communication.Responses;
using ItemService.API.Domain.Entities;

namespace ItemService.API.Communication.Profiles;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<Item, ItemJsonResponse>();
        CreateMap<ItemJsonRequest, Item>();
    }
}
