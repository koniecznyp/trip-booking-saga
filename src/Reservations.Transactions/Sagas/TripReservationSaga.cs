using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Sagas
{
    public class TripReservationSaga : Saga,
        ISagaStartAction<CarBooked>
    {
        private readonly IBusPublisher _busPublisher;
        
        public TripReservationSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task CompensateAsync(CarBooked message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(CarBooked message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}