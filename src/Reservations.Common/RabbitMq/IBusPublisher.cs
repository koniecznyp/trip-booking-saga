using System.Threading.Tasks;
using Reservations.Common.Events;

namespace Reservations.Common.RabbitMq
{
    public interface IBusPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}