using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.RabbitMq;
using Reservations.Transactions.Messages.CarsRental.Commands;
using Reservations.Transactions.Messages.CarsRental.Events;
using Reservations.Transactions.Messages.Hotels.Commands;
using Reservations.Transactions.Messages.Hotels.Events;

namespace Reservations.Transactions.Sagas
{
    public class TripReservationSaga : Saga,
        ISagaStartAction<CarReservationCreated>,
        ISagaAction<CreateCarReservationRejected>,
        ISagaAction<HotelReservationCreated>,
        ISagaAction<CreateHotelReservationRejected>
    {
        private readonly IBusPublisher _busPublisher;
        
        public TripReservationSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CarReservationCreated message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new CreateHotelReservation(message.UserId, message.StartDate, message.EndDate),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task CompensateAsync(CarReservationCreated message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new CancelCarReservation(message.ReservationId),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task HandleAsync(CreateCarReservationRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(CreateCarReservationRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(HotelReservationCreated message, ISagaContext context)
        {
            Complete();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(HotelReservationCreated message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(CreateHotelReservationRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(CreateHotelReservationRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }
    }
}