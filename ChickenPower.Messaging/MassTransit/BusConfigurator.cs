using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace ChickenPower.Messaging.MassTransit
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(
                    new Uri(RabbitMqConnectionInformation.Uri),
                    hst =>
                    {
                        hst.Username(RabbitMqConnectionInformation.Username);
                        hst.Password(RabbitMqConnectionInformation.Password);
                    });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}