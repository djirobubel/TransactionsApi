using MediatR;

namespace TransactionsApi.CQRS.Queries.AccrualClientBalance
{
    public class AccrualClientBalanceQuery : IRequest<AccrualClientBalanceResult>
    {
        public Guid ClientId { get; set; }

        public AccrualClientBalanceQuery(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
