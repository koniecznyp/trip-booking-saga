using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reservations.Common.Commands;
using Reservations.Common.Events;
using Reservations.Common.RabbitMq;
using Reservations.Services.Flights.Handlers;
using Reservations.Services.Flights.Messages.Commands;
using Reservations.Services.Flights.Messages.Events;

namespace Reservations.Services.Flights
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
            services.AddScoped<ICommandHandler<CreateFlightReservation>, CreateFlightReservationHandler>();
            services.AddScoped<IBusPublisher, BusPublisher>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRabbitMq()
                .SubscribeCommand<CreateFlightReservation>(onError: ex 
                    => new CreateFlightReservationRejected(ex.Message));
            app.UseMvc();
        }
    }
}
