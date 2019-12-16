using System;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Commands
{
    [MessageNamespace("hotels")]
    public class CancelHotelReservation : ICommand
    {
        public Guid ReservationId { get; set; }

        public CancelHotelReservation(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}