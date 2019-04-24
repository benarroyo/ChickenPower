using ChickenPower.Common.Logging;
using ChickenPower.Messaging.MassTransit;
using ChickenPower.PricingProxy.Services;
using MassTransit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChickenPower.PricingProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggingConfiguration.ConfigureLogger("PricingProxy");

            var bus = ConfigureBus();
            bus.Start();
            CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private static IBusControl ConfigureBus()
        {
            return BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.UseSerilog();
                cfg.ReceiveEndpoint(
                    host,
                    "pricing_service_data",
                    endpointConfigurator =>
                    {
                        endpointConfigurator.Consumer(() => new PricingProxyConsumer());
                    }); ;
            });
        }
    }
}
