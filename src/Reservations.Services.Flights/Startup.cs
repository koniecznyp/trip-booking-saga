using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using Reservations.Common.Jaeger;
using Reservations.Common.RabbitMq;
using Reservations.Services.Flights.Handlers;
using Reservations.Services.Flights.Messages.Commands;
using Reservations.Services.Flights.Messages.Events;

namespace Reservations.Services.Flights
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
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddJaeger();
            builder.AddRabbitMq();
            builder.RegisterType<BusPublisher>().As<IBusPublisher>();
            builder.RegisterType<CreateFlightReservationHandler>()
                .As<ICommandHandler<CreateFlightReservation>>();
            
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRabbitMq()
                .SubscribeCommand<CreateFlightReservation>(onError: ex 
                    => new CreateFlightReservationRejected(ex.Message));
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
