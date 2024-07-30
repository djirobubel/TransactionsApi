using MediatR;

namespace TransactionsApi.CQRS.Commands.Accrual
{
    public class AccrualCommand : IRequest<AccrualResult>
    {
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}
