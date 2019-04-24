using ChickenPower.Messaging.Events;

namespace ChickenPower.ContractGeneratorProxy.Events
{
    public class ContractGenerationFinishedEvent : IContractGenerationFinished
    {
        public string ProposalId { get; set; }

        public ContractGenerationFinishedEvent(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}