using TransactionsApi.Models;

namespace TransactionsApi.Interface
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(Transaction transaction);
        Task<ICollection<Transaction>> GetTransactionsByClientIdAsync(Guid clientId, int pageNumber,
            int pageSize, string sortBy, bool isAscending);
        Task<decimal> GetClientCurrentBalanceAsync(Guid clientId);
    }
}
