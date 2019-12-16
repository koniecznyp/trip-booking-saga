using System;
using Reservations.Common.Events;

namespace Reservations.Services.Hotels.Messages.Events
{
    public class HotelReservationCanceled : IEvent
    {
        public Guid ReservationId { get; set; }
        
        public HotelReservationCanceled(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}