namespace ChickenPower.StateSaga.Events
{
    public class PriceRequestExpiredEvent
    {
        public string ProposalId { get; }

        public PriceRequestExpiredEvent(string proposalId)
        {
            ProposalId = proposalId;
        }
    }
}