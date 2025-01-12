
using ItemService.API.Application.UseCases;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace ItemService.API.ExternalServices.RabbitMQ;

public class RabbitMQSubscriber : BackgroundService
{
    private readonly Task _createAsync;
    private readonly IConfiguration _configuration;
    private readonly ProcessQueueMessageUseCase _processQueueMessageUseCase;
    private string? _queueName;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitMQSubscriber(IConfiguration configuration, ProcessQueueMessageUseCase processQueueMessageUseCase)
    {
        _configuration = configuration;
        _createAsync = InitializeAsync();
        _processQueueMessageUseCase = processQueueMessageUseCase;
    }

    private async Task InitializeAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"]!,
            Port = int.Parse(_configuration["RabbitMQPort"]!)
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);
        _queueName = (await _channel.QueueDeclareAsync()).QueueName;
        await _channel.QueueBindAsync(queue: _queueName, exchange: "trigger", routingKey: string.Empty);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _createAsync;
        if (_channel == null)
            throw new Exception("Failed to initialize RabbitMQ client");
        if (_queueName == null)
            throw new Exception("Failed to initialize RabbitMQ client");


        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
             
                await _processQueueMessageUseCase.Execute(message);

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");

                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
            }
        };

        await _channel.BasicConsumeAsync(_queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);
    }
}
