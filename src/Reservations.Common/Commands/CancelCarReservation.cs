using System;

namespace Reservations.Common.Commands
{
    public class CancelCarReservation : ICommand
    {
        public Guid ReservationId { get; set; }

        public CancelCarReservation(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}