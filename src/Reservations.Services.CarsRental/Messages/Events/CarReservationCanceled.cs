using System;
using Reservations.Common.Events;

namespace Reservations.Services.CarsRental.Messages.Events
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