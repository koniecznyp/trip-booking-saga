namespace Reservations.Common.Events
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