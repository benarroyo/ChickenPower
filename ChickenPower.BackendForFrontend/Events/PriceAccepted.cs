using ChickenPower.Messaging.Events;

namespace ChickenPower.BackendForFrontend.Events
{
    public class PriceAccepted: IPriceRequested
    {
        public string ProposalId { get; set; }

        public PriceAccepted(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}