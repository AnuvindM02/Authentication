using Authentication.Domain.Entities;
namespace Authentication.Application.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole, CancellationToken cancellationToken);
    }
}
