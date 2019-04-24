namespace ChickenPower.Messaging.Events
{
    public interface IPriceCalulationFinished
    {
        string ProposalId { get; set; }

        decimal Price { get; }
    }
}