using System.Threading.Tasks;
using Reservations.Common.RabbitMq;

namespace Reservations.Common.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}