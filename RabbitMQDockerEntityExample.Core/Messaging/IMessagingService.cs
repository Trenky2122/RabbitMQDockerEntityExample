using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace RabbitMQDockerEntityExample.Core.Messaging;

public interface IMessagingService
{
    void SendMessage(object message);
    void SubscribeMessages(Action<object?, BasicDeliverEventArgs> actionOnEventRaise);
}