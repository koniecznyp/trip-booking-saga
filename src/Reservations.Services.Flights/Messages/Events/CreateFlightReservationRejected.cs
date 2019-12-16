using Reservations.Common.Events;

namespace Reservations.Services.Flights.Messages.Events
{
    public class CreateFlightReservationRejected : IRejectedEvent
    {
        public string Message { get; set; }

        public CreateFlightReservationRejected(string message)
        {
            Message = message;
        }
    }
}