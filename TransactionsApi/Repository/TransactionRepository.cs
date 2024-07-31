using Microsoft.EntityFrameworkCore;
using TransactionsApi.Data;
using TransactionsApi.Interface;
using TransactionsApi.Models;

namespace TransactionsApi.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            return await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetClientCurrentBalanceAsync(Guid clientId)
        {
            return await _context.Transactions.Where(c => c.ClientId == clientId).
                SumAsync(a => a.Amount);
        }

        public async Task<ICollection<Transaction>> GetTransactionsByClientIdAsync(Guid clientId)
        {
            return await _context.Transactions.Where(t => t.ClientId == clientId).ToListAsync();
        }
    }
}
