using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.CarsRental.Events
{
    [MessageNamespace("cars_rental")]
    public class CancelCarReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CancelCarReservationRejected(string message)
        {
            Message = message;
        }
    }
}