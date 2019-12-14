namespace Reservations.Common.Events
{
    public class BookHotelRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public BookHotelRejected(string message)
        {
            Message = message;
        }
    }
}