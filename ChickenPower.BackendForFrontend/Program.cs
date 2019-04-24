using Autofac.Extensions.DependencyInjection;
using ChickenPower.Common.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChickenPower.BackendForFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggingConfiguration.ConfigureLogger("BackendForFrontend");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();
    }
}
