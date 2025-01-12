using RestaurantService.API.Communication.Responses;

namespace RestaurantService.API.ExternalServices.RabbitMQ;

public interface IRabbitMQClient
{
    public Task PubRestaurantReference(RestaurantJsonResponse content);
}
