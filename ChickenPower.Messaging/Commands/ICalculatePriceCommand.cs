namespace ChickenPower.Messaging.Commands
{
    public interface ICalculatePriceCommand
    {
        string ProposalId { get; set; }
    }
}