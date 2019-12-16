using System;
using Reservations.Common.Events;

namespace Reservations.Services.Flights.Messages.Events
{
    public class FlightReservationCreated : IEvent
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public FlightReservationCreated(Guid userId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}