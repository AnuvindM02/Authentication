using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Authentication.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Client?> GetByClientIdAsync(string clientId, CancellationToken cancellationToken)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId, cancellationToken);
        }
    }
}
