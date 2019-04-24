using System;
using System.Threading.Tasks;
using ChickenPower.BackendForFrontend.Events;
using ChickenPower.Messaging.Events;
using ChickenPower.Messaging.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ChickenPower.BackendForFrontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ProposalController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }


        [Route("new")]
        [HttpPost]
        public async Task<ActionResult> NewProposal([FromBody]string proposalId)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMqConnectionInformation.ProposalSagaEndpoint));
            await endpoint.Send<INewProposalRequested>(new NewProposalRequested(proposalId));

            return Ok();
        }


        [Route("price")]
        [HttpPost]
        public async Task<ActionResult> RequestPrice([FromBody]string proposalId)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMqConnectionInformation.ProposalSagaEndpoint));
            await endpoint.Send<IPriceRequested>(new PriceRequested(proposalId));

            return Ok();
        }

        [Route("accept")]
        [HttpPost]
        public async Task<ActionResult> AcceptPrice([FromBody]string proposalId)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMqConnectionInformation.ProposalSagaEndpoint));
            await endpoint.Send<IPriceAccepted>(new PriceAccepted(proposalId));

            return Ok();
        }
    }
}
