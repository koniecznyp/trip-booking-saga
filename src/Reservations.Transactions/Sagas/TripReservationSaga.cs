using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.RabbitMq;
using Reservations.Transactions.Messages.Api;
using Reservations.Transactions.Messages.CarsRental.Commands;
using Reservations.Transactions.Messages.CarsRental.Events;
using Reservations.Transactions.Messages.Commands;
using Reservations.Transactions.Messages.Flights.Commands;
using Reservations.Transactions.Messages.Flights.Events;
using Reservations.Transactions.Messages.Hotels.Commands;
using Reservations.Transactions.Messages.Hotels.Events;

namespace Reservations.Transactions.Sagas
{
    public class TripReservationSaga : Saga,
        ISagaStartAction<CreateReservation>,
        ISagaAction<CarReservationCreated>,
        ISagaAction<CreateCarReservationRejected>,
        ISagaAction<HotelReservationCreated>,
        ISagaAction<CreateHotelReservationRejected>,
        ISagaAction<FlightReservationCreated>,
        ISagaAction<CreateFlightReservationRejected>
    {
        private readonly IBusPublisher _busPublisher;
        
        public TripReservationSaga(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateReservation message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new CreateCarReservation(message.UserId, message.StartDate, message.EndDate),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task CompensateAsync(CreateReservation message, ISagaContext context)
            => await Task.CompletedTask;

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
            => await RejectAsync();

        public async Task CompensateAsync(CreateCarReservationRejected message, ISagaContext context)
            => await Task.CompletedTask;

        public async Task HandleAsync(HotelReservationCreated message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new CreateFlightReservation(message.UserId, message.StartDate, message.EndDate),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task CompensateAsync(HotelReservationCreated message, ISagaContext context)
        {
            await _busPublisher.SendAsync(new CancelHotelReservation(message.ReservationId),
                CorrelationContext.FromId(context.CorrelationId));
        }

        public async Task HandleAsync(CreateHotelReservationRejected message, ISagaContext context)
            => await RejectAsync();

        public async Task CompensateAsync(CreateHotelReservationRejected message, ISagaContext context)
            => await Task.CompletedTask;

        public async Task HandleAsync(FlightReservationCreated message, ISagaContext context)
            => await CompleteAsync();

        public async Task CompensateAsync(FlightReservationCreated message, ISagaContext context)
            => await Task.CompletedTask;

        public async Task HandleAsync(CreateFlightReservationRejected message, ISagaContext context)
            => await RejectAsync();

        public async Task CompensateAsync(CreateFlightReservationRejected message, ISagaContext context)
            => await Task.CompletedTask;
    }
}