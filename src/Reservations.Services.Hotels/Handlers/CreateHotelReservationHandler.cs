using System;
using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.Hotels.Messages.Commands;
using Reservations.Services.Hotels.Messages.Events;

namespace Reservations.Services.Cars.Handlers
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
            throw new Exception("some test problem with hotel booking...");
            await _busPublisher.PublishAsync(new HotelReservationCreated(command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}