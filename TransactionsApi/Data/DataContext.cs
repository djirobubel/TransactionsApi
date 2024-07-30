using Microsoft.EntityFrameworkCore;
using TransactionsApi.Models;

namespace TransactionsApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
           .HasOne(c => c.Client)
           .WithMany(t => t.Transactions)
           .HasForeignKey(c => c.ClientId);
        }
    }
}
