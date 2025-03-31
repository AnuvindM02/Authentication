using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // DbSet representing the Users table.
        public required DbSet<User> Users { get; set; }
        // DbSet representing the Roles table.
        public required DbSet<Role> Roles { get; set; }
        // DbSet representing the Clients table.
        public required DbSet<Client> Clients { get; set; }
        // DbSet representing the UserRoles join table.
        public required DbSet<UserRole> UserRoles { get; set; }
        // DbSet representing the SigningKeys table.
        public required DbSet<SigningKey> SigningKeys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
