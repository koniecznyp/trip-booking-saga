using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
            builder.RegisterType<CreateHotelReservationHandler>()
                .As<ICommandHandler<CreateHotelReservation>>();
            builder.RegisterType<CancelHotelReservationHandler>()
                .As<ICommandHandler<CancelHotelReservation>>();
                
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
                .SubscribeCommand<CreateHotelReservation>(onError: ex 
                    => new CreateHotelReservationRejected(ex.Message))
                .SubscribeCommand<CancelHotelReservation>(onError: ex 
                    => new CancelHotelReservationRejected(ex.Message));
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
