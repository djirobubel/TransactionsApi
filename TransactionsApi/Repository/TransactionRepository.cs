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

        public async Task<ICollection<Transaction>> GetTransactionsByClientIdAsync(Guid clientId,
            int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            var skipNumber = (pageNumber - 1) * pageSize;

            IQueryable<Transaction> query = _context.Transactions.Where(c => c.ClientId == clientId);

            query = sortBy.ToLower() switch
            {
                "amount" => isAscending
                    ? query.OrderBy(a => a.Amount)
                    : query.OrderByDescending(a => a.Amount),
                "transactiondate" => isAscending
                    ? query.OrderBy(t => t.TransactionDate)
                    : query.OrderByDescending(t => t.TransactionDate),
                _ => query.OrderByDescending(t => t.TransactionDate)
            };

            return await query.Skip(skipNumber).Take(pageSize).ToListAsync();
        }
    }
}
