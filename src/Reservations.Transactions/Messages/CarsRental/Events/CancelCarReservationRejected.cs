using Reservations.Common.Events;

namespace Reservations.Transactions.Messages.CarsRental.Events
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