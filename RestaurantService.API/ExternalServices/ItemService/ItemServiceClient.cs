using RestaurantService.API.Communication.Responses;
using System.Text.Json;
using System.Text;

namespace RestaurantService.API.ExternalServices.ItemService;

public class ItemServiceClient : IItemServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ItemServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendRestaurantReferenceToItemService(RestaurantJsonResponse content)
    {
        var contentHttp = new StringContent
                    (
                      JsonSerializer.Serialize(content),
                        Encoding.UTF8,
                        "application/json"
                    );
        var serviceUrl = _configuration["MicroservicesHosts:ItemService"] + "/RestaurantReference";
        var result = await _httpClient.PostAsync(serviceUrl, contentHttp);
    }
}
