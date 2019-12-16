using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.CarsRental.Events
{
    [MessageNamespace("cars_rental")]
    public class CreateCarReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CreateCarReservationRejected(string message)
        {
            Message = message;
        }
    }
}