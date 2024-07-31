using MediatR;

namespace TransactionsApi.CQRS.Commands.Accrual
{
    public class AccrualCommand : IRequest<AccrualCommandResult>
    {
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}
