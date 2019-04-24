using ChickenPower.Messaging.Events;

namespace ChickenPower.BackendForFrontend.Events
{
    public class NewProposalRequested : INewProposalRequested
    {
        public string ProposalId { get; set; }

        public NewProposalRequested(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}