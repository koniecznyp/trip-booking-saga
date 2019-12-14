using System;
using Reservations.Common.Commands;
using Reservations.Common.Events;

namespace Reservations.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(Func<Exception, IRejectedEvent> onError = null) 
            where TCommand : ICommand;
            
        IBusSubscriber SubscribeEvent<TEvent>(Func<Exception, IRejectedEvent> onError = null) 
            where TEvent : IEvent;
    }
}