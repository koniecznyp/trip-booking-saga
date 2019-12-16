using System;
using Reservations.Common.Commands;

namespace Reservations.Transactions.Messages.CarsRental.Commands
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