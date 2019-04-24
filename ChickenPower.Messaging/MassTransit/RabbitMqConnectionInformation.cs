namespace ChickenPower.Messaging.MassTransit
{
    public static class RabbitMqConnectionInformation
    {
        public static string Uri { get; } = "rabbitmq://10.0.75.1:5672/SheepPower";

        public static string Username { get; } = "guest";

        public static string Password { get; } = "guest";

        public static string ProposalSagaEndpoint { get; } = "rabbitmq://10.0.75.1:5672/SheepPower/proposal_saga";

        public static string ContractGeneratorEndpoint { get; } = "rabbitmq://10.0.75.1:5672/SheepPower/contract_generator_data";

        public static string PricingServiceEndpoint { get; } = "rabbitmq://10.0.75.1:5672/SheepPower/pricing_service_data";
    }
}
