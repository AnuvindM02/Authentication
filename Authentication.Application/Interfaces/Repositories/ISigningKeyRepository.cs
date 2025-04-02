using System.Linq.Expressions;
using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces.Repositories
{
    public interface ISigningKeyRepository
    {
        Task<SigningKey?> GetActiveSigningKey(CancellationToken cancellationToken);
        Task<List<SigningKey>?> GetAllByCondition(Expression<Func<SigningKey, bool>> predicate, CancellationToken cancellationToken);
    }
}
