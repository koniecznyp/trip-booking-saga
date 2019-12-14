using System;
using System.Threading.Tasks;
using RawRabbit;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Services.Cars.Handlers
{
    public class BookHotelHandler : ICommandHandler<BookHotel>
    {
        private readonly IBusPublisher _busPublisher;

        public BookHotelHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(BookHotel command, ICorrelationContext context)
        {
            throw new Exception("some test problem with hotel booking...");
            await _busPublisher.PublishAsync(new HotelBooked(command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}