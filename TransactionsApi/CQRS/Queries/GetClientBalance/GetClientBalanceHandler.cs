using MediatR;
using TransactionsApi.Interface;

namespace TransactionsApi.CQRS.Queries.GetClientBalance
{
    public class GetClientBalanceHandler : IRequestHandler<GetClientBalanceQuery,
        GetClientBalanceResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public GetClientBalanceHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<GetClientBalanceResult> Handle(GetClientBalanceQuery request,
            CancellationToken cancellationToken)
        {
            if (!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Client not found.");
            }

            var transactions = _transactionRepository.GetTransactionsByClientIdAsync(request.ClientId);
            decimal currentBalance = 0;
            foreach (var transaction in await transactions)
            {
                currentBalance += transaction.Amount;
            }

            return new GetClientBalanceResult { Balance = currentBalance };
        }
    }
}
