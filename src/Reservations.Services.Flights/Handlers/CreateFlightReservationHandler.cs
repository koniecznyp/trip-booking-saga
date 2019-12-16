using System;
using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.Flights.Messages.Commands;
using Reservations.Services.Flights.Messages.Events;

namespace Reservations.Services.Flights.Handlers
{
    public class CreateFlightReservationHandler : ICommandHandler<CreateFlightReservation>
    {
        private readonly IBusPublisher _busPublisher;

        public CreateFlightReservationHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateFlightReservation command, ICorrelationContext context)
        {
            throw new Exception("some test problem with flight booking...");
            await _busPublisher.PublishAsync(new FlightReservationCreated(command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}