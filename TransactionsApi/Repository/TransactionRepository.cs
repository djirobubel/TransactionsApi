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

        public async Task<ICollection<Transaction>> GetTransactionsByClientIdAsync(Guid clientId)
        {
            IQueryable<Transaction> transactions = _context.Transactions;
            return await transactions.Where(t => t.ClientId == clientId).ToListAsync();
        }
    }
}
