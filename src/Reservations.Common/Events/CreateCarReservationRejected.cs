namespace Reservations.Common.Events
{
    public class CreateCarReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CreateCarReservationRejected(string message)
        {
            Message = message;
        }
    }
}