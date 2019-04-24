namespace ChickenPower.Messaging.Events
{
    public interface IPriceRequested
    {
        string ProposalId { get; set; }
    }
}