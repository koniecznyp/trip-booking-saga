using System.Threading.Tasks;
using RawRabbit;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Services.Cars.Handlers
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