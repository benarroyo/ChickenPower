using ChickenPower.Messaging.Events;

namespace ChickenPower.PricingProxy.Events
{
    public class PriceCalulationFinished : IPriceCalulationFinished
    {
        public string ProposalId { get; set; }

        public decimal Price { get; }

        public PriceCalulationFinished(string proposalId, decimal price)
        {
            ProposalId = proposalId;
            Price = price;
        }
    }
}