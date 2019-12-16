using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Reservations.Api.Commands;
using Reservations.Common.RabbitMq;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IBusPublisher _busPublisher;

        public ReservationsController(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReservation command)
        {
            var id = Guid.NewGuid();
            var context = new CorrelationContext(id, command.UserId, "reservations");
            await _busPublisher.SendAsync(command, context);
            return Accepted($"reservations/{id}");
        }
    }
}