using System;
using Reservations.Common.Commands;

namespace Reservations.Services.Hotels.Messages.Commands
{
    public class CancelHotelReservation : ICommand
    {
        public Guid ReservationId { get; set; }

        public CancelHotelReservation(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}