using System;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.CarsRental.Commands
{
    [MessageNamespace("cars_rental")]
    public class CancelCarReservation : ICommand
    {
        public Guid ReservationId { get; set; }

        public CancelCarReservation(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}