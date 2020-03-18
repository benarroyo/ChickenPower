using ChickenPower.Common.Logging;
using ChickenPower.Messaging.MassTransit;
using ChickenPower.Persistence;
using ChickenPower.StateSaga.StateSaga;
using MassTransit;
using MassTransit.Context;
using MassTransit.Saga;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog.Extensions.Logging;


namespace ChickenPower.StateSaga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LoggingConfiguration.ConfigureLogger("StateSaga");
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
            var orderStateMachine = new ProposalStateMachine();

            var repository = new InMemorySagaRepository<PersistedProposal>();

            return BusConfigurator.ConfigureBus(cfg =>
            {
                cfg.ReceiveEndpoint(
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
