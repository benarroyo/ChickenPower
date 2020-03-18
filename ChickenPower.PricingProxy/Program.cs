using ChickenPower.Common.Logging;
using ChickenPower.Messaging.MassTransit;
using ChickenPower.PricingProxy.Services;
using MassTransit;
using MassTransit.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog.Extensions.Logging;

namespace ChickenPower.PricingProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LoggingConfiguration.ConfigureLogger("PricingProxy");
            LogContext.ConfigureCurrentLogContext(new SerilogLoggerFactory(logger));

            var bus = ConfigureBus();
            bus.Start();
            CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private static IBusControl ConfigureBus()
        {
            return BusConfigurator.ConfigureBus(cfg =>
            {
                cfg.ReceiveEndpoint(
                    "pricing_service_data",
                    endpointConfigurator =>
                    {
                        endpointConfigurator.Consumer(() => new PricingProxyConsumer());
                    }); ;
            });
        }
    }
}
