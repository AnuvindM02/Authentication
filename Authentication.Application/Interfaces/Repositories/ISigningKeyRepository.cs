using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces.Repositories
{
    public interface ISigningKeyRepository
    {
        Task<SigningKey?> GetActiveSigningKey(CancellationToken cancellationToken);
    }
}
