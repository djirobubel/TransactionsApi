using MediatR;

namespace TransactionsApi.CQRS.Commands.WriteOff
{
    public class WriteOffCommand : IRequest<WriteOffCommandResult>
    {
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}
