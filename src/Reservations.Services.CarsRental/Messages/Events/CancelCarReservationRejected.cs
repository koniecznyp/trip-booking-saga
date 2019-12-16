using Reservations.Common.Events;

namespace Reservations.Services.CarsRental.Messages.Events
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