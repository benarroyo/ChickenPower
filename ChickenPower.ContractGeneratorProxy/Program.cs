using ChickenPower.Common.Logging;
using ChickenPower.ContractGeneratorProxy.Services;
using ChickenPower.Messaging.MassTransit;
using MassTransit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChickenPower.ContractGeneratorProxy
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
                    "contract_generator_data",
                    endpointConfigurator =>
                    {
                        endpointConfigurator.Consumer(() => new ContractGeneratorConsumer());
                    }); ;
            });
        }
    }
}
