using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Handlers
{
    public class EventHandler<T> : IEventHandler<T> where T : class, IEvent
    {
        private readonly ISagaCoordinator _sagaCoordinator;
        
        public EventHandler(ISagaCoordinator sagaCoordinator)
        {
            _sagaCoordinator = sagaCoordinator;
        }

        public async Task HandleAsync(T @event, ICorrelationContext context)
        {
            var sagaContext = Sagas.SagaContext.FromCorrelationContext(context);
            await _sagaCoordinator.ProcessAsync(@event, sagaContext);
        }
    }
}