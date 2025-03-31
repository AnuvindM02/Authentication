using Authentication.Domain.Entities;
namespace Authentication.Application.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetByClientIdAsync(string clientId, CancellationToken cancellationToken);
    }
}
