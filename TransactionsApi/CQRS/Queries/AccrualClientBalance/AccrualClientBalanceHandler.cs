using MediatR;
using TransactionsApi.Interface;

namespace TransactionsApi.CQRS.Queries.AccrualClientBalance
{
    public class AccrualClientBalanceHandler : IRequestHandler<AccrualClientBalanceQuery,
        AccrualClientBalanceResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public AccrualClientBalanceHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<AccrualClientBalanceResult> Handle(AccrualClientBalanceQuery request,
            CancellationToken cancellationToken)
        {
            if (!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Client not found.");
            }

            var currentBalance = await _transactionRepository.
                GetClientCurrentBalanceAsync(request.ClientId);

            return new AccrualClientBalanceResult { Balance = currentBalance };
        }
    }
}
