using System;
using Reservations.Common.Events;

namespace Reservations.Services.Hotels.Messages.Events
{
    public class HotelReservationCreated : IEvent
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public HotelReservationCreated(Guid reservationId, Guid userId, DateTime startDate, DateTime endDate)
        {
            ReservationId = reservationId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}