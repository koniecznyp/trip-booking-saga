using System.Threading.Tasks;
using RawRabbit;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Services.Cars.Handlers
{
    public class BookCarHandler : ICommandHandler<BookCar>
    {
        private readonly IBusPublisher _busPublisher;

        public BookCarHandler(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(BookCar command, ICorrelationContext context)
        {
            await _busPublisher.PublishAsync(new CarBooked(command.UserId, command.StartDate, command.EndDate), context);
        }
    }
}