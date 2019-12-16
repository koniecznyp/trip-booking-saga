using Reservations.Common.Events;

namespace Reservations.Services.CarsRental.Messages.Events
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