using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.CarsRental.Handlers;
using Reservations.Services.CarsRental.Messages.Commands;
using Reservations.Services.CarsRental.Messages.Events;

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
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateCarReservation>, CreateCarReservationHandler>();
            services.AddScoped<ICommandHandler<CancelCarReservation>, CancelCarReservationHandler>();
            services.AddScoped<IBusPublisher, BusPublisher>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRabbitMq()
                .SubscribeCommand<CreateCarReservation>(onError: ex 
                    => new CreateCarReservationRejected(ex.Message))
                .SubscribeCommand<CancelCarReservation>(onError: ex 
                    => new CancelCarReservationRejected(ex.Message));
            app.UseMvc();
        }
    }
}
