using Chronicle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;
using Reservations.Transactions.Handlers;
using Reservations.Transactions.Messages.Api;
using Reservations.Transactions.Messages.CarsRental.Events;
using Reservations.Transactions.Messages.Hotels.Events;

namespace Reservations.Services.Cars
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddChronicle();
            services.AddRabbitMq(Configuration);
            services.AddScoped(typeof(ICommandHandler<>), typeof(CommandHandler<>));
            services.AddScoped(typeof(IEventHandler<>), typeof(EventHandler<>));
            services.AddScoped<IBusPublisher, BusPublisher>();
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
                .SubscribeEvent<CreateHotelReservationRejected>();
            app.UseMvc();
        }
    }
}
