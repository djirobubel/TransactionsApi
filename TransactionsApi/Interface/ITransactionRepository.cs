using TransactionsApi.Models;

namespace TransactionsApi.Interface
{
    public interface ITransactionRepository
    {
        Task<int> CreateTransactionAsync(Transaction transaction);
        Task<ICollection<Transaction>> GetTransactionsByClientIdAsync(Guid clientId);
        Task<decimal> GetClientCurrentBalanceAsync(Guid clientId);
    }
}
