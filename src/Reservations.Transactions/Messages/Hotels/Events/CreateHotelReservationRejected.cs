using Reservations.Common.Events;

namespace Reservations.Transactions.Messages.Hotels.Events
{
    public class CreateHotelReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CreateHotelReservationRejected(string message)
        {
            Message = message;
        }
    }
}