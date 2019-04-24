namespace ChickenPower.Messaging.Events
{
    public interface INewProposalRequested
    {
        string ProposalId { get; set; }
    }
}