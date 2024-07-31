using MediatR;

namespace TransactionsApi.CQRS.Queries.GetClientBalance
{
    public class GetClientBalanceQuery : IRequest<GetClientBalanceResult>
    {
        public Guid ClientId { get; set; }

        public GetClientBalanceQuery(Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
