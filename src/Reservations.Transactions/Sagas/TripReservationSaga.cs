using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Sagas
{
    public class TripReservationSaga : Saga,
        ISagaStartAction<CarReserved>,
        ISagaAction<CarReservationRejected>,
        ISagaAction<HotelReserved>,
        ISagaAction<HotelReservationRejected>
    {
        private readonly IBusPublisher _busPublisher;
        
        public TripReservationSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CarReserved message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new CreateHotelReservation(message.UserId, message.StartDate, message.EndDate),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task CompensateAsync(CarReserved message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(CarReservationRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(CarReservationRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(HotelReserved message, ISagaContext context)
        {
            Complete();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(HotelReserved message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(HotelReservationRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(HotelReservationRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }
    }
}