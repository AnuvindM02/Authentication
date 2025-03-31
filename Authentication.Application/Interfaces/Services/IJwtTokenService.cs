using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(User user, Client client, CancellationToken cancellationToken);
    }
}
