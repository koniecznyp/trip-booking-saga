using System;
using Reservations.Common.Commands;

namespace Reservations.Common.Commands
{
    public class CreateReservation : ICommand
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}