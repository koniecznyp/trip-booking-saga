using Reservations.Common.Events;

namespace Reservations.Services.Hotels.Messages.Events
{
    public class CancelHotelReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CancelHotelReservationRejected(string message)
        {
            Message = message;
        }
    }
}