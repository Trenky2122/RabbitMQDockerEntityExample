using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQDockerEntityExample.Core.Messaging;

public sealed class MessagingService : IMessagingService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string QueueName = "ExampleQueue";
    private const string ExchangeName = "ExampleExchange";
    private const string RoutingKey = "ExampleRoutingKey";

    public MessagingService(Settings.Settings settings)
    {
        var factory = new ConnectionFactory()
        {
            HostName = settings.MessagingServiceSettings.RabbitMQHostname,
            UserName = settings.MessagingServiceSettings.RabbitMQUsername,
            Password = settings.MessagingServiceSettings.RabbitMQPassword,
        };
        bool tryConnecting = true;
        DateTime timeConnectingStarted = DateTime.UtcNow;
        while (tryConnecting)
        {
            // we wait for RabbitMQ container to become available
            try
            {
                _connection = factory.CreateConnection();
                tryConnecting = false;
            }
            catch
            {
                if (DateTime.UtcNow - timeConnectingStarted > TimeSpan.FromSeconds(settings.MessagingServiceSettings.RabbitMQWaitSecond))
                    throw; // probably something went wrong
            }
        }
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: QueueName, exclusive: false);
        _channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");
        _channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);
    }

    public void SendMessage(object message)
    {
        var messageJson = System.Text.Json.JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageJson);
        _channel.BasicPublish(exchange: ExchangeName,
            routingKey: RoutingKey,
            body: body);
    }

    public void SubscribeMessages(Action<object?, BasicDeliverEventArgs> actionOnEventRaise)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            actionOnEventRaise(model, ea); // if action throws, message will be redelivered
            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume(queue: QueueName,
            autoAck: false,
            consumer: consumer);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}