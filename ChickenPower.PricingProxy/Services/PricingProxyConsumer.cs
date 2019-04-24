using System;
using System.Threading.Tasks;
using ChickenPower.Messaging.Commands;
using ChickenPower.Messaging.Events;
using ChickenPower.PricingProxy.Events;
using MassTransit;
using Serilog;

namespace ChickenPower.PricingProxy.Services
{
    public class PricingProxyConsumer : IConsumer<IGenerateContractCommand>
    {
        public async Task Consume(ConsumeContext<IGenerateContractCommand> context)
        {
            var proposalId = context.Message.ProposalId;

            Log.Information("[Pricing Service] Received price request for Proposal: ProposalId=[{ProposalId}]", proposalId);

            await Task
                .Delay(10000)
                .ContinueWith(async _ =>
                {
                    var price = GeneratePrice();
                    await context.Publish<IPriceCalulationFinished>(new PriceCalulationFinished(proposalId, price));
                });
        }



        private static readonly Random randomPriceGenerator = new Random();

        private static decimal GeneratePrice()
        {
            return (decimal) randomPriceGenerator.Next(5, 20) / 100;
        }
    }
}