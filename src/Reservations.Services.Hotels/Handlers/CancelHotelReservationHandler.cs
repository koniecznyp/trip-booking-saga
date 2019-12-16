using System;
using System.Threading.Tasks;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.Hotels.Messages.Commands;
using Reservations.Services.Hotels.Messages.Events;

namespace Reservations.Services.Hotels.Handlers
{
    public class CancelHotelReservationHandler : ICommandHandler<CancelHotelReservation>
    {
        private readonly IBusPublisher _busPublisher;

        public CancelHotelReservationHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CancelHotelReservation command, ICorrelationContext context)
        {
            // some logic to cancel hotel reservation based on HotelReservationId
            await _busPublisher.PublishAsync(new HotelReservationCanceled(command.ReservationId), context);
        }
    }
}