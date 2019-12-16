using System;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Messages.Api
{
    [MessageNamespace("reservations_api")]
    public class CreateReservation : ICommand
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}