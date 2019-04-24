using ChickenPower.Messaging.Commands;

namespace ChickenPower.StateSaga.Commands
{
    public class GenerateContractCommand : IGenerateContractCommand
    {
        public string ProposalId { get; set; }

        public GenerateContractCommand(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}