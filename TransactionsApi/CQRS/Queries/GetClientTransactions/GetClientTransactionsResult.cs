using TransactionsApi.Dto;

namespace TransactionsApi.CQRS.Queries.GetClientTransactions
{
    public class GetClientTransactionsResult
    {
        public List<TransactionDto>? Transactions { get; set; }
    }
}
