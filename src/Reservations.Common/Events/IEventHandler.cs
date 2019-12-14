using System.Threading.Tasks;

namespace Reservations.Common.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}