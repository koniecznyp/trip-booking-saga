using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.Events;

namespace Reservations.Common.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) where TCommand : ICommand;
        Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context) where TEvent : IEvent;
    }
}