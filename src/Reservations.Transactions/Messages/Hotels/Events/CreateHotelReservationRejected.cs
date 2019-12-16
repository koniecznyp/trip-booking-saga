using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Hotels.Events
{
    [MessageNamespace("hotels")]
    public class CreateHotelReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CreateHotelReservationRejected(string message)
        {
            Message = message;
        }
    }
}