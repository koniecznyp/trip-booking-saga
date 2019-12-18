using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using RawRabbit;
using Reservations.Api.Commands;
using Reservations.Common.RabbitMq;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ITracer _tracer;

        public ReservationsController(IBusPublisher busPublisher, ITracer tracer)
        {
            _busPublisher = busPublisher;
            _tracer = tracer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReservation command)
        {
            var context = new CorrelationContext(Guid.NewGuid(), command.UserId, "reservations",
                _tracer.ActiveSpan.Context.ToString());
            await _busPublisher.SendAsync(command, context);
            return Accepted($"reservations/{context.Id}");
        }
    }
}