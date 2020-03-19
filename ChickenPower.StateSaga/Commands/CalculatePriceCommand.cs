using ChickenPower.Messaging.Commands;

namespace ChickenPower.StateSaga.Commands
{
    public class CalculatePriceCommand : ICalculatePriceCommand
    {
        public string ProposalId { get; set; }

        public CalculatePriceCommand(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}