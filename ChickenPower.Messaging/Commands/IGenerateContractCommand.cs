namespace ChickenPower.Messaging.Commands
{
    public interface IGenerateContractCommand
    {
        string ProposalId { get; set; }
    }
}