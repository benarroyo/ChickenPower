using Serilog.Core;
using Serilog.Events;

namespace ChickenPower.Common.Logging
{
    public class ServiceNameEnricher : ILogEventEnricher
    {
        private readonly string _serviceName;

        public ServiceNameEnricher(string serviceName)
        {
            _serviceName = serviceName;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "Service", _serviceName));
        }
    }
}