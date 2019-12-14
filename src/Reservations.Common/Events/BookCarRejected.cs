namespace Reservations.Common.Events
{
    public class BookCarRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public BookCarRejected(string message)
        {
            Message = message;
        }
    }
}