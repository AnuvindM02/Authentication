using System.Linq.Expressions;
using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class SigningKeyRepository(ApplicationDbContext _context) : ISigningKeyRepository
    {
        public async Task<SigningKey?> GetActiveSigningKey(CancellationToken cancellationToken)
        {
            return await _context.SigningKeys.FirstOrDefaultAsync(k => k.IsActive, cancellationToken);
        }

        public async Task<List<SigningKey>?> GetAllByCondition(Expression<Func<SigningKey, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.SigningKeys.Where(predicate).ToListAsync(cancellationToken);
        }
    }
}
