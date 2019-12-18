using System;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Flights.Events
{
    [MessageNamespace("flights")]
    public class FlightReservationCreated : IEvent
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public FlightReservationCreated(Guid reservationId, Guid userId, DateTime startDate, DateTime endDate)
        {
            ReservationId = reservationId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}