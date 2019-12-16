using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Hotels.Events
{
    [MessageNamespace("hotels")]
    public class CancelHotelReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CancelHotelReservationRejected(string message)
        {
            Message = message;
        }
    }
}