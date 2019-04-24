using Serilog;
using Serilog.Core;

namespace ChickenPower.Common.Logging
{
    public static class LoggingConfiguration
    {
        public static string SeqUrl { get; } = "http://10.0.75.1:5341";

        public static Logger ConfigureLogger(string serviceName)
        {
            var configuration = new LoggerConfiguration()
                .Enrich.With(new ServiceNameEnricher(serviceName))
                .WriteTo.Console()
                .WriteTo.Seq(SeqUrl)
                .CreateLogger();
            Log.Logger = configuration;

            return configuration;
        }
    }
}
