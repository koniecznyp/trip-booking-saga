using System;
using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.Hotels.Messages.Commands;
using Reservations.Services.Hotels.Messages.Events;

namespace Reservations.Services.Hotels.Handlers
{
    public class CreateHotelReservationHandler : ICommandHandler<CreateHotelReservation>
    {
        private readonly IBusPublisher _busPublisher;

        public CreateHotelReservationHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateHotelReservation command, ICorrelationContext context)
        {
            var reservationId = Guid.NewGuid();
            // some logic with hotel reservation...
            await _busPublisher.PublishAsync(new HotelReservationCreated(reservationId, command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}