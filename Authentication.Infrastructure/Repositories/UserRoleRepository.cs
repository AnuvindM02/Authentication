using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
namespace Authentication.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken)
        {
            await _context.UserRoles.AddAsync(userRole);
            _context.SaveChanges();
        }
    }
}
