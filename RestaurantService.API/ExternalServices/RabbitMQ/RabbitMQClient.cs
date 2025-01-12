using RabbitMQ.Client;
using RestaurantService.API.Communication.Responses;
using System.Text;
using System.Text.Json;

namespace RestaurantService.API.ExternalServices.RabbitMQ;

public class RabbitMQClient : IRabbitMQClient, IAsyncDisposable
{
    // This Initialization is specific for this case where the RabbitMQ connection method was updated to Async
    private readonly Task CreateAsync;
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMQClient(IConfiguration configuration)
    {
        _configuration = configuration;
        CreateAsync = InitializeAsync(); 
    }

    private async Task InitializeAsync()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQHost"]!,
                Port = int.Parse(_configuration["RabbitMQPort"]!)
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize RabbitMQ client: {ex.Message}");
            throw; 
        }
    }


    public async Task PubRestaurantReference(RestaurantJsonResponse content)
    {
        await CreateAsync;
        var message = JsonSerializer.Serialize(content);
        var body = Encoding.UTF8.GetBytes(message);

        await _channel!.BasicPublishAsync(
            exchange: "trigger", 
            routingKey: string.Empty, 
            body: body);
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
        }

        if (_connection is not null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
    }
}