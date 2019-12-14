using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Reservations.Common.Commands;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IBusClient _busClient;
        public ReservationsController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReservation command)
        {
            await _busClient.PublishAsync(new BookCar(){ UserId = command.UserId, StartDate = command.StartDate, EndDate = command.EndDate });
            return Accepted("cars/test1");
        }
    }
}