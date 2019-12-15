namespace Reservations.Common.Events
{
    public class HotelReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public HotelReservationRejected(string message)
        {
            Message = message;
        }
    }
}