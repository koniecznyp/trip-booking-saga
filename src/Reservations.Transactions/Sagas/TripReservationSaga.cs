using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Sagas
{
    public class TripReservationSaga : Saga,
        ISagaStartAction<CarBooked>,
        ISagaAction<BookCarRejected>,
        ISagaAction<HotelBooked>,
        ISagaAction<BookHotelRejected>
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
            await Task.CompletedTask;
        }

        public async Task HandleAsync(BookCarRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(BookCarRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(HotelBooked message, ISagaContext context)
        {
            Complete();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(HotelBooked message, ISagaContext context)
        {
            await Task.CompletedTask;
        }

        public async Task HandleAsync(BookHotelRejected message, ISagaContext context)
        {
            Reject();
            await Task.CompletedTask;
        }

        public async Task CompensateAsync(BookHotelRejected message, ISagaContext context)
        {
            await Task.CompletedTask;
        }
    }
}