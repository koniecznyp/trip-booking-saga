using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Common.Commands;
using Reservations.Common.RabbitMq;
using Reservations.Services.CarsRental.Handlers;
using Reservations.Services.CarsRental.Messages.Commands;
using Reservations.Services.CarsRental.Messages.Events;

namespace Reservations.Services.CarsRental
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
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddRabbitMq();
            builder.RegisterType<BusPublisher>().As<IBusPublisher>();
            builder.RegisterType<CreateCarReservationHandler>()
                .As<ICommandHandler<CreateCarReservation>>();
            builder.RegisterType<CancelCarReservationHandler>()
                .As<ICommandHandler<CancelCarReservation>>();
            
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
                .SubscribeCommand<CreateCarReservation>(onError: ex 
                    => new CreateCarReservationRejected(ex.Message))
                .SubscribeCommand<CancelCarReservation>(onError: ex 
                    => new CancelCarReservationRejected(ex.Message));
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
