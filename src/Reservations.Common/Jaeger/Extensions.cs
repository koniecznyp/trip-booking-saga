using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using RawRabbit.Instantiation;
using OpenTracing;
using OpenTracing.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Autofac;

namespace Reservations.Common.Jaeger
{
    public static class Extensions
    {
        public static void AddJaeger(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = new JaegerOptions();
                configuration.GetSection("jaeger").Bind(options);
                return options;
            }).SingleInstance();

            builder.Register<ITracer>(context =>
            {
                var loggerFactory = context.Resolve<ILoggerFactory>();
                var options = context.Resolve<JaegerOptions>();

                var reporter = new RemoteReporter
                        .Builder()
                    .WithSender(new UdpSender(options.UdpHost, options.UdpPort, options.MaxPacketSize))
                    .WithLoggerFactory(loggerFactory)
                    .Build();

                var sampler = GetSampler(options);
                var tracer = new Tracer
                        .Builder(options.ServiceName)
                    .WithReporter(reporter)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);
                return tracer;
            }).SingleInstance();
        }

        public static IClientBuilder UseJaeger(this IClientBuilder builder, ITracer tracer)
        {
            builder.Register(pipe => pipe
                .Use<JaegerStagedMiddleware>(tracer));
            return builder;
        }

        private static ISampler GetSampler(JaegerOptions options)
        {
            switch (options.Sampler)
            {
                case "const": return new ConstSampler(true);
                case "rate": return new RateLimitingSampler(options.MaxTracesPerSecond);
                case "probabilistic": return new ProbabilisticSampler(options.SamplingRate);
                default: return new ConstSampler(true);
            }
        }
    }
}