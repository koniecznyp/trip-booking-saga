using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Sagas
{
    public class TripReservationSaga : Saga,
        ISagaStartAction<CarBooked>,
        ISagaAction<HotelBooked>
    {
        private readonly IBusPublisher _busPublisher;
        
        public TripReservationSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CarBooked message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new BookHotel(message.UserId, message.StartDate, message.EndDate),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task CompensateAsync(CarBooked message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(HotelBooked message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task CompensateAsync(HotelBooked message, ISagaContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}