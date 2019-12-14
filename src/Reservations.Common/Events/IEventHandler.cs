using System.Threading.Tasks;
using Reservations.Common.RabbitMq;

namespace Reservations.Common.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T @event, ICorrelationContext context);
    }
}