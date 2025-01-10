using AutoMapper;
using ItemService.API.Communication.Requests;
using ItemService.API.Communication.Responses;
using ItemService.API.Domain.Entities;
using ItemService.API.Domain.IRepositories;

namespace ItemService.API.Application.UseCases;

public class RegisterItemUseCase
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public RegisterItemUseCase(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<ItemJsonResponse> Execute(ItemJsonRequest request)
    {
        var result = await _itemRepository.Create(_mapper.Map<Item>(request));
        await _itemRepository.SaveChanges();
        return _mapper.Map<ItemJsonResponse>(result);
    }
}
