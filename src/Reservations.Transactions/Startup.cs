using Chronicle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;
using Reservations.Transactions.Handlers;

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
                .SubscribeEvent<CarReserved>()
                .SubscribeEvent<HotelReserved>()
                .SubscribeEvent<CarReservationRejected>()
                .SubscribeEvent<HotelReservationRejected>();
            app.UseMvc();
        }
    }
}
