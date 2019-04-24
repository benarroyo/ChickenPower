using System;
using Automatonymous;

namespace ChickenPower.Persistence
{
    public class PersistedProposal : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public string ProposalId { get; set; }

        public string CurrentState { get; set; }

        public Guid? PricingExpirationId { get; set; }

        public decimal Price { get; set; }
    }
}
