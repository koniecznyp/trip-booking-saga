using System.Threading.Tasks;
using RawRabbit;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Services.Cars.Handlers
{
    public class CreateCarReservationHandler : ICommandHandler<CreateCarReservation>
    {
        private readonly IBusPublisher _busPublisher;

        public CreateCarReservationHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateCarReservation command, ICorrelationContext context)
        {
            await _busPublisher.PublishAsync(new CarReserved(command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}