using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.CarsRental.Messages.Commands;
using Reservations.Services.CarsRental.Messages.Events;

namespace Reservations.Services.CarsRental.Handlers
{
    public class CancelCarReservationHandler : ICommandHandler<CancelCarReservation>
    {
        private readonly IBusPublisher _busPublisher;

        public CancelCarReservationHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CancelCarReservation command, ICorrelationContext context)
        {
            // some logic to cancel reservation based on ReservationId
            await _busPublisher.PublishAsync(new CarReservationCanceled(command.ReservationId), context);
        }
    }
}