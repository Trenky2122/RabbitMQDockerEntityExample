using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQDockerEntityExample.Core.Messaging
{
    public sealed class MessagingService : IMessagingService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueName = "ExampleQueue";
        private const string ExchangeName = "ExampleExchange";
        private const string RoutingKey = "ExampleRoutingKey";

        public MessagingService()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, exclusive: false);
            _channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");
            _channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);
        }

        public void SendMessage(object message)
        {
            var messageJson = System.Text.Json.JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageJson);
            _channel.BasicPublish(exchange: QueueName,
                routingKey: RoutingKey,
                body: body);
        }

        public void SubscribeMessages(Action<object?, BasicDeliverEventArgs> action)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                action(model, ea);
            };
            _channel.BasicConsume(queue: QueueName,
                autoAck: true,
                consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
