using System;
using Reservations.Common.Events;

namespace Reservations.Transactions.Messages.Hotels.Events
{
    public class HotelReservationCreated : IEvent
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public HotelReservationCreated(Guid userId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}