using System;
using Reservations.Common.Commands;

namespace Reservations.Services.CarsRental.Messages.Commands
{
    public class CreateCarReservation : ICommand
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CreateCarReservation(Guid userId, DateTime startDate, DateTime endDate)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}