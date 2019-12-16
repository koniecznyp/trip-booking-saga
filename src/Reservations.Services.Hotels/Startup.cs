using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.Hotels.Handlers;
using Reservations.Services.Hotels.Messages.Commands;
using Reservations.Services.Hotels.Messages.Events;

namespace Reservations.Services.Hotels
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
            services.AddScoped<ICommandHandler<CreateHotelReservation>, CreateHotelReservationHandler>();
            services.AddScoped<ICommandHandler<CancelHotelReservation>, CancelHotelReservationHandler>();
            services.AddScoped<IBusPublisher, BusPublisher>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRabbitMq()
                .SubscribeCommand<CreateHotelReservation>(onError: ex 
                    => new CreateHotelReservationRejected(ex.Message))
                .SubscribeCommand<CancelHotelReservation>(onError: ex 
                    => new CancelHotelReservationRejected(ex.Message));
            app.UseMvc();
        }
    }
}
