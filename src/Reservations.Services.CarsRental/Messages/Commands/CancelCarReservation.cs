using System;
using Reservations.Common.Commands;

namespace Reservations.Services.CarsRental.Messages.Commands
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