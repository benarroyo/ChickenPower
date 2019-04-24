using ChickenPower.Messaging.Events;

namespace ChickenPower.BackendForFrontend.Events
{
    public class PriceRequested : IPriceRequested
    {
        public string ProposalId { get; set; }

        public PriceRequested(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}