using Microsoft.EntityFrameworkCore;
using TransactionsApi.Data;
using TransactionsApi.Interface;

namespace TransactionsApi.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClientExists(Guid clientId)
        {
            return _context.Clients.Any(c => c.Id == clientId);
        }
    }
}
