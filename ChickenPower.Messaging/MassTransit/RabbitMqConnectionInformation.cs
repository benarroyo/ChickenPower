namespace ChickenPower.Messaging.MassTransit
{
    public static class RabbitMqConnectionInformation
    {
        public static string Uri { get; } = "rabbitmq://rabbit-mq:5672/ChickenPower";

        public static string Username { get; } = "guest";

        public static string Password { get; } = "guest";

        public static string ProposalSagaEndpoint { get; } = "rabbitmq://rabbit-mq:5672/ChickenPower/proposal_saga";

        public static string ContractGeneratorEndpoint { get; } = "rabbitmq://rabbit-mq:5672/ChickenPower/contract_generator_data";

        public static string PricingServiceEndpoint { get; } = "rabbitmq://rabbit-mq:5672/ChickenPower/pricing_service_data";
    }
}
