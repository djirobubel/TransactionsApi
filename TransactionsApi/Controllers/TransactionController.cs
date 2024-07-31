using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionsApi.CQRS.Commands.Accrual;
using TransactionsApi.CQRS.Commands.WriteOff;
using TransactionsApi.CQRS.Commands.Transfer;
using TransactionsApi.CQRS.Queries.GetClientBalance;

namespace TransactionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(GetClientBalanceResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetClientBalance(Guid clientId)
        {
            var result = await _mediator.Send(new GetClientBalanceQuery(clientId));
            return Ok(result);
        }

        [HttpPost("Accrual")]
        [ProducesResponseType(typeof(AccrualCommandResult), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Accrual([FromBody] AccrualCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("WriteOff")]
        [ProducesResponseType(typeof(WriteOffCommandResult), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> WriteOff([FromBody] WriteOffCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Transfer")]
        [ProducesResponseType(typeof(TransferCommandResult), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Transfer([FromBody] TransferCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
