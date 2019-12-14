using System;

namespace Reservations.Common.Commands
{
    public class BookCar : ICommand
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}