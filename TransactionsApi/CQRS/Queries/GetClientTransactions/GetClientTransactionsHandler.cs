using MediatR;
using TransactionsApi.Dto;
using TransactionsApi.Interface;

namespace TransactionsApi.CQRS.Queries.GetClientTransactions
{
    public class GetClientTransactionsHandler : IRequestHandler<GetClientTransactionsQuery,
        GetClientTransactionsResult>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientRepository _clientRepository;

        public GetClientTransactionsHandler(ITransactionRepository transactionRepository,
            IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<GetClientTransactionsResult> Handle(GetClientTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            if (!_clientRepository.ClientExists(request.ClientId))
            {
                throw new InvalidOperationException("Unable to perform transfer. Client not found.");
            }

            var transactions = await _transactionRepository.GetTransactionsByClientIdAsync
                (request.ClientId, request.PageNumber, request.PageSize, request.SortBy,
                request.IsAscending);

            var transactionsDto = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Comment = t.Comment,
                TransactionDate = DateTime.UtcNow,
            }).ToList();

            var result = new GetClientTransactionsResult { Transactions = transactionsDto };

            return result;
        }
    }
}
