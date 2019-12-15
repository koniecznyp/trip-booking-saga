using System;

namespace Reservations.Common.Events
{
    public class CarReservationCanceled : IEvent
    {
        public Guid ReservationId { get; set; }

        public CarReservationCanceled(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}