using Reservations.Common.Events;

namespace Reservations.Services.Hotels.Messages.Events
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