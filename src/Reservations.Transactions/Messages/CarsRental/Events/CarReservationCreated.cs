using System;
using Reservations.Common.Events;

namespace Reservations.Transactions.Messages.CarsRental.Events
{
    public class CarReservationCreated : IEvent
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public CarReservationCreated(Guid reservationId, Guid userId, DateTime startDate, DateTime endDate)
        {
            ReservationId = reservationId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}