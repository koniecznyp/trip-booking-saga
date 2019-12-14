using System;

namespace Reservations.Common.Events
{
    public class CarBooked : IEvent
    {
        public Guid Id { get; set; }
        
        public CarBooked(Guid id)
        {
            Id = id;
        }
    }
}