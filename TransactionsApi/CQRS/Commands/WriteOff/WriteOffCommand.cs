using MediatR;

namespace TransactionsApi.CQRS.Commands.WriteOff
{
    public class WriteOffCommand : IRequest<WriteOffResult>
    {
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}
