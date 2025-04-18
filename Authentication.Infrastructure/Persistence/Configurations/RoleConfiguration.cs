using System.Reflection.Emit;
using Authentication.Domain.Entities;
using Authentication.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table name (optional)
            builder.ToTable("Roles");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Unique constraint for Name (if roles like "Admin", "User" should be unique)
            builder.HasIndex(r => r.Name)
                   .HasDatabaseName("IX_Unique_RoleName")
                   .IsUnique();

            // Property configurations
            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.Description)
                   .HasMaxLength(255);

            // Relationships: One Role can have many UserRoles
            builder.HasMany(r => r.UserRoles)
                   .WithOne(ur => ur.Role)
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Role { Id = (int)RoleType.Admin, Name = RoleType.Admin.ToString(), Description = $"{RoleType.Admin.ToString()} Role" },
                new Role { Id = (int)RoleType.User, Name = RoleType.User.ToString(), Description = $"{RoleType.User.ToString()} Role" },
                new Role { Id = (int)RoleType.Editor, Name = RoleType.Editor.ToString(), Description = $"{RoleType.Editor.ToString()} Role" },
                new Role { Id = (int)RoleType.Viewer, Name = RoleType.Viewer.ToString(), Description = $"{RoleType.Viewer.ToString()} Role" }
            );
        }

    }
}
