using System.Threading.Tasks;
using ChickenPower.ContractGeneratorProxy.Events;
using ChickenPower.Messaging.Commands;
using ChickenPower.Messaging.Events;
using MassTransit;
using Serilog;

namespace ChickenPower.ContractGeneratorProxy.Services
{
    public class ContractGeneratorConsumer : IConsumer<IGenerateContractCommand>
    {
        public async Task Consume(ConsumeContext<IGenerateContractCommand> context)
        {
            var proposalId = context.Message.ProposalId;

            Log.Information("[Contract Generator] Received contract generation request for Proposal: ProposalId=[{ProposalId}]", proposalId);

            await Task
                .Delay(10000)
                .ContinueWith(async _ =>
                {
                    await context.Publish<IContractGenerationFinished>(new ContractGenerationFinishedEvent(proposalId));
                });
        }
    }
}