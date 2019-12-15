namespace Reservations.Common.Events
{
    public class CancelCarReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CancelCarReservationRejected(string message)
        {
            Message = message;
        }
    }
}