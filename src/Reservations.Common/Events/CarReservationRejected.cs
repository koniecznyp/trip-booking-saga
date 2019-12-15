namespace Reservations.Common.Events
{
    public class CarReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CarReservationRejected(string message)
        {
            Message = message;
        }
    }
}