using System.Linq.Expressions;
using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByConditionAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _context.Roles.FirstOrDefaultAsync(predicate);
        }
    }
}
