using Authentication.Domain.Entities;

namespace Authentication.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        Task<bool> EmailExistsAsync(string email, int userId, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
        Task<List<User>?> GetAllUsers(DateTimeOffset? cursor, int limit, string? search, CancellationToken cancellationToken);
    }
}
