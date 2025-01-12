using RestaurantService.API.Communication.Responses;

namespace RestaurantService.API.ExternalServices.ItemService;

public interface IItemServiceClient
{
    public Task SendRestaurantReferenceToItemService(RestaurantJsonResponse content);
}
