using Reservations.Common.Commands;
using Reservations.Common.Events;

namespace Reservations.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>() where TCommand : ICommand;
        IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent;
    }
}