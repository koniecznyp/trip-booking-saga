using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using OpenTracing.Tag;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Enrichers.MessageContext;
using Reservations.Common.Commands;
using Reservations.Common.Events;

namespace Reservations.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBusClient _busClient;
        private readonly ITracer _tracer;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
            _tracer = _serviceProvider.GetService<ITracer>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>(Func<Exception, IRejectedEvent> onError = null) 
            where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
                return await TryHandleAsync(command, correlationContext,
                    () => commandHandler.HandleAsync(command, correlationContext), onError);
            });
            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(Func<Exception, IRejectedEvent> onError = null) 
            where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent, CorrelationContext>(async (@event, correlationContext) => {
                var handler = _serviceProvider.GetService<IEventHandler<TEvent>>();
                return await TryHandleAsync(@event, correlationContext, 
                    () => handler.HandleAsync(@event, correlationContext), onError);
            });
            return this;
        }

        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message, 
            CorrelationContext correlationContext, Func<Task> handle,
            Func<Exception, IRejectedEvent> onError = null)
        {
            var messageName = message.GetType().Name;
            var scope = _tracer
                .BuildSpan("message-handler")
                .AsChildOf(_tracer.ActiveSpan)
                .StartActive(true);
            
            using(scope)
            {
                var span = scope.Span;
                try
                {
                    span.Log($"START {messageName} with correlation context id: {correlationContext.Id}");
                    await handle();
                    span.Log($"END {messageName} with correlation context id: {correlationContext.Id}");
                    return new Ack();
                }
                catch(Exception ex)
                {
                    if (onError != null)
                    {
                        span.Log(ex.Message);
                        span.SetTag(Tags.Error, true);
                        var rejectedEvent = onError(ex);
                        await _busClient.PublishAsync(rejectedEvent, ctx => ctx.UseMessageContext(correlationContext));
                        return new Ack();
                    }
                    throw new Exception($"Unable to handle a message: '{messageName}'");
                }
            }
        }
    }
}