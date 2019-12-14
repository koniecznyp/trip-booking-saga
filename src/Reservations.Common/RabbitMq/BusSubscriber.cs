using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Common;
using Reservations.Common.Commands;
using Reservations.Common.Events;

namespace Reservations.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBusClient _busClient;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>() where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
                return await TryHandleAsync(command, correlationContext,
                    () => commandHandler.HandleAsync(command, correlationContext));
            });
            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent, CorrelationContext>(async (@event, correlationContext) => {
                var handler = _serviceProvider.GetService<IEventHandler<TEvent>>();
                return await TryHandleAsync(@event, correlationContext, 
                    () => handler.HandleAsync(@event, correlationContext));
            });
            return this;
        }

        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message, 
            CorrelationContext correlationContext, Func<Task> handle)
        {
            try
            {
                await handle();
                return new Ack();
            }
            catch(Exception ex)
            {
                var messageName = message.GetType().Name;
                throw new Exception($"Unable to handle a message: '{messageName}'");
            }
        }
    }
}