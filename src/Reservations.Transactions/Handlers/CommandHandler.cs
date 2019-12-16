using System.Threading.Tasks;
using Chronicle;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;

namespace Reservations.Transactions.Handlers
{
    public class CommandHandler<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ISagaCoordinator _sagaCoordinator;
        
        public CommandHandler(ISagaCoordinator sagaCoordinator)
        {
            _sagaCoordinator = sagaCoordinator;
        }

        public async Task HandleAsync(T command, ICorrelationContext context)
        {
            var sagaContext = Sagas.SagaContext.FromCorrelationContext(context);
            await _sagaCoordinator.ProcessAsync(command, sagaContext);
        }
    }
}