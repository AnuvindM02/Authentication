using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class SigningKeyRepository : ISigningKeyRepository
    {
        private readonly ApplicationDbContext _context;
        public SigningKeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SigningKey?> GetActiveSigningKey(CancellationToken cancellationToken)
        {
            return await _context.SigningKeys.FirstOrDefaultAsync(k => k.IsActive, cancellationToken);
        }
    }
}
