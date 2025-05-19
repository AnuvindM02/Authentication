using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Application.Interfaces.Repositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext _context) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> EmailExistsAsync(string email, int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && (u.Id != userId || userId == 0), cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<User>?> GetAllUsers(DateTimeOffset? cursor, int limit, string? search, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var loweredSearch = search.ToLower();

                query = query.Where(u =>
                    EF.Functions.ILike(u.FirstName, $"{loweredSearch}%") ||
                    (u.Lastname != null && EF.Functions.ILike(u.Lastname, $"{loweredSearch}%")) ||
                    EF.Functions.ILike(u.Email, $"{loweredSearch}%"));
            }

            if(cursor.HasValue)
            {
                query = query.Where(u => u.CreatedAt < cursor.Value);
            }

            var users = await query.OrderByDescending(u => u.CreatedAt)
                .Take(limit)
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
