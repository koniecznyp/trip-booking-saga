using System;
using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.CarsRental.Messages.Commands;
using Reservations.Services.CarsRental.Messages.Events;

namespace Reservations.Services.CarsRental.Handlers
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
            var reservationId = Guid.NewGuid(); // generate some unique car reservation Id
            await _busPublisher.PublishAsync(new CarReservationCreated(reservationId, command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}