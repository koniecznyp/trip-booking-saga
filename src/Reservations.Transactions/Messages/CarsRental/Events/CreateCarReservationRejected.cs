using Reservations.Common.Events;

namespace Reservations.Transactions.Messages.CarsRental.Events
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