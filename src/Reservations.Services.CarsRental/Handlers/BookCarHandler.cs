using System.Threading.Tasks;
using RawRabbit;
using Reservations.Common.Commands;
using Reservations.Common.Events;

namespace Reservations.Services.Cars.Handlers
{
    public class BookCarHandler : ICommandHandler<BookCar>
    {
        private readonly IBusClient _busClient;

        public BookCarHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(BookCar command)
        {
            await _busClient.PublishAsync(new CarBooked(command.UserId));
        }
    }
}