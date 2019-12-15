using System;

namespace Reservations.Common.Events
{
    public class HotelReserved : IEvent
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public HotelReserved(Guid userId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}