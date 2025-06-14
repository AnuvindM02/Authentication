using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Authentication.Infrastructure.Repositories
{
    public class ClientRepository(ApplicationDbContext _context) : IClientRepository
    {
        public async Task<Client?> GetByClientIdAsync(string clientId, CancellationToken cancellationToken)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId, cancellationToken);
        }
        public async Task<Client?> GetFirst(CancellationToken cancellationToken)
        {
            return await _context.Clients.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
