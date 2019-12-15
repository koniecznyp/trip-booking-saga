using System;
using System.Threading.Tasks;
using RawRabbit;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

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