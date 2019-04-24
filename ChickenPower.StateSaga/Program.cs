using ChickenPower.Common.Logging;
using ChickenPower.Messaging.MassTransit;
using ChickenPower.Persistence;
using ChickenPower.StateSaga.StateSaga;
using MassTransit;
using MassTransit.Saga;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace ChickenPower.StateSaga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggingConfiguration.ConfigureLogger("StateSaga");

            var bus = ConfigureBus();
            bus.Start();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        private static IBusControl ConfigureBus()
        {
            var orderStateMachine = new ProposalStateMachine();

            var repository = new InMemorySagaRepository<PersistedProposal>();

            return BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.UseSerilog();
                cfg.ReceiveEndpoint(
                    host,
                    "proposal_saga",
                    e =>
                    {
                        e.UseInMemoryOutbox();
                        e.StateMachineSaga(orderStateMachine, repository);
                    });
                cfg.UseInMemoryScheduler();
            });
        }
    }
}
