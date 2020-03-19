// ReSharper disable UnusedAutoPropertyAccessor.Local
using System;
using Automatonymous;
using ChickenPower.Messaging.Events;
using ChickenPower.Messaging.MassTransit;
using ChickenPower.Persistence;
using ChickenPower.StateSaga.Commands;
using ChickenPower.StateSaga.Events;
using MassTransit;
using Serilog;

namespace ChickenPower.StateSaga.StateSaga
{
    public class ProposalStateMachine : MassTransitStateMachine<PersistedProposal>
    {
        public State GeneratingContract { get; private set; }

        public State ContractGenerated { get; private set; }

        public State CalculatingPrice { get; private set; }

        public State PriceCalculated { get; private set; }

        public State ContractSigned { get; private set; }


        public Event<INewProposalRequested> NewProposalRequested { get; private set; }

        public Event<IContractGenerationFinished> ContractGenerationFinished { get; private set; }

        public Event<IPriceRequested> PriceRequested { get; private set; }

        public Event<IPriceCalulationFinished> PriceCalulationFinished { get; private set; }

        public Event<IPriceAccepted> PriceAccepted { get; private set; }

        public Schedule<PersistedProposal, PriceRequestExpiredEvent> PriceRequestExpired { get; private set; }


        public ProposalStateMachine()
        {
            InstanceState(s => s.CurrentState);


            Event(() => NewProposalRequested,
                x =>
                {
                    x.CorrelateBy(sagaState => sagaState.ProposalId, context => context.Message.ProposalId);
                    x.InsertOnInitial = true;
                    x.SetSagaFactory(context => new PersistedProposal
                    {
                        CorrelationId = NewId.NextGuid(),
                        ProposalId = context.Message.ProposalId
                    });
                    x.SelectId(context => NewId.NextGuid());
                });

            Event(() => ContractGenerationFinished,
                x => x.CorrelateBy(sagaState => sagaState.ProposalId, context => context.Message.ProposalId));

            Event(() => PriceRequested,
                x => x.CorrelateBy(sagaState => sagaState.ProposalId, context => context.Message.ProposalId));

            Event(() => PriceCalulationFinished,
                x => x.CorrelateBy(sagaState => sagaState.ProposalId, context => context.Message.ProposalId));

            Event(() => PriceAccepted,
                x => x.CorrelateBy(sagaState => sagaState.ProposalId, context => context.Message.ProposalId));

            Schedule(() => PriceRequestExpired,
                x => x.PricingExpirationId,
                x =>
                {
                    x.Delay = TimeSpan.FromSeconds(20);
                    x.Received = e => e.CorrelateBy(sagaState => sagaState.ProposalId, context => context.Message.ProposalId);
                });


            Initially(
                When(NewProposalRequested)
                    .Then(
                        context =>
                            Log.Information("[ProposalStateMachine] New Proposal: Id=[{CorrelationId}] ProposalId=[{ProposalId}]",
                                context.Instance.CorrelationId,
                                context.Data.ProposalId))
                    .Send((state, command) => new Uri(RabbitMqConnectionInformation.ContractGeneratorEndpoint),
                        context => new GenerateContractCommand(context.Data.ProposalId))
                    .TransitionTo(GeneratingContract));


            During(GeneratingContract,
                When(ContractGenerationFinished)
                    .Then(
                        context =>
                            Log.Information("[ProposalStateMachine] Contract generated: Id=[{CorrelationId}] ProposalId=[{ProposalId}]",
                                context.Instance.CorrelationId,
                                context.Data.ProposalId))
                    .TransitionTo(ContractGenerated));


            During(ContractGenerated, PriceCalculated,
                When(PriceRequested)
                    .Then(
                        context =>
                            Log.Information("[ProposalStateMachine] Price requested: Id=[{CorrelationId}] ProposalId=[{ProposalId}]",
                                context.Instance.CorrelationId,
                                context.Data.ProposalId))
                    .Send((state, command) => new Uri(RabbitMqConnectionInformation.PricingServiceEndpoint),
                        context => new CalculatePriceCommand(context.Data.ProposalId))
                    .TransitionTo(CalculatingPrice)
                    .Schedule(PriceRequestExpired, context => new PriceRequestExpiredEvent(context.Data.ProposalId)),
                Ignore(PriceCalulationFinished));


            During(CalculatingPrice,
                When(PriceCalulationFinished)
                    .Then(context => {
                            Log.Information("[ProposalStateMachine] Price calculated: Id=[{CorrelationId}] ProposalId=[{ProposalId}] Price=[{Price}]",
                                context.Instance.CorrelationId,
                                context.Data.ProposalId,
                                context.Data.Price);
                            context.Instance.Price = context.Data.Price;
                        })
                    .Unschedule(PriceRequestExpired)
                    .TransitionTo(PriceCalculated),
                When(PriceRequestExpired.Received)
                    .Then(context =>
                            Log.Information("[ProposalStateMachine] Price calculation expired: Id=[{CorrelationId}] ProposalId=[{ProposalId}]",
                                context.Instance.CorrelationId,
                                context.Data.ProposalId))
                    .TransitionTo(ContractGenerated));


            During(PriceCalculated,
                When(PriceAccepted)
                    .Then(context =>
                            Log.Information("[ProposalStateMachine] Price accepted: Id=[{CorrelationId}] ProposalId=[{ProposalId}] Price=[{price}]",
                                context.Instance.CorrelationId,
                                context.Data.ProposalId,
                                context.Instance.Price))
                    .TransitionTo(ContractSigned));
        }
    }
}