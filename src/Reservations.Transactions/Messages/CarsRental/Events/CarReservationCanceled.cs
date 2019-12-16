using System;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.CarsRental.Events
{
    [MessageNamespace("cars_rental")]
    public class CarReservationCanceled : IEvent
    {
        public Guid ReservationId { get; set; }

        public CarReservationCanceled(Guid reservationId)
        {
            ReservationId = reservationId;
        }
    }
}