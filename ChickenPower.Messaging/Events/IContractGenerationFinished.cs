namespace ChickenPower.Messaging.Events
{
    public interface IContractGenerationFinished
    {
        string ProposalId { get; set; }
    }
}