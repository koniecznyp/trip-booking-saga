using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Flights.Events
{
    [MessageNamespace("flights")]
    public class CreateFlightReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CreateFlightReservationRejected(string message)
        {
            Message = message;
        }
    }
}