using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Chronicle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.Jaeger;
using Reservations.Common.RabbitMq;
using Reservations.Transactions.Handlers;
using Reservations.Transactions.Messages.Api;
using Reservations.Transactions.Messages.CarsRental.Events;
using Reservations.Transactions.Messages.Flights.Events;
using Reservations.Transactions.Messages.Hotels.Events;

namespace Reservations.Transactions
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOpenTracing();
            services.AddChronicle();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddJaeger();
            builder.AddRabbitMq();
            builder.RegisterType<BusPublisher>().As<IBusPublisher>();
            builder.RegisterGeneric(typeof(CommandHandler<>))
                .As(typeof(ICommandHandler<>));
            builder.RegisterGeneric(typeof(Handlers.EventHandler<>))
                .As(typeof(IEventHandler<>));
            
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRabbitMq()
                .SubscribeCommand<CreateReservation>()
                
                .SubscribeEvent<CarReservationCreated>()
                .SubscribeEvent<CreateCarReservationRejected>()
                .SubscribeEvent<CarReservationCanceled>()
                .SubscribeEvent<CancelCarReservationRejected>()

                .SubscribeEvent<HotelReservationCreated>()
                .SubscribeEvent<CreateHotelReservationRejected>()
                .SubscribeEvent<HotelReservationCanceled>()
                .SubscribeEvent<CancelHotelReservationRejected>()

                .SubscribeEvent<FlightReservationCreated>()
                .SubscribeEvent<CreateFlightReservationRejected>();
            app.UseMvc();
        }
    }
}
