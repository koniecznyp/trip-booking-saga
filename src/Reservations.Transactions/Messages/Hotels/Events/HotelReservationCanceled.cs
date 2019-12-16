using System;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Hotels.Events
{
    [MessageNamespace("hotels")]
    public class HotelReservationCanceled : IEvent
    {
        public Guid ReservationId { get; set; }
        
        public HotelReservationCanceled(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}