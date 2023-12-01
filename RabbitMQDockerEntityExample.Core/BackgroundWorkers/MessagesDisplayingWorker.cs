using Microsoft.Extensions.Hosting;
using RabbitMQDockerEntityExample.Core.Messaging;
using System.Text;

namespace RabbitMQDockerEntityExample.Core.BackgroundWorkers
{
    public class MessagesDisplayingWorker: BackgroundService
    {
        private readonly IMessagingService _messagingService;
        public MessagesDisplayingWorker(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messagingService.SubscribeMessages((model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            });
            return Task.CompletedTask;
        }
    }
}
