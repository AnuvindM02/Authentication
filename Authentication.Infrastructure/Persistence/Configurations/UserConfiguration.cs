using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name (optional, if different from class name)
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Unique Index for Email
            builder.HasIndex(u => u.Email)
                   .HasDatabaseName("IX_Unique_Email")
                   .IsUnique();

            builder.Property(u => u.CreatedAt)
                    .IsRequired();

            builder.HasIndex(u => u.CreatedAt)
                    .HasDatabaseName("IX_CreatedAt");

            // Property Configurations
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Middlename)
                   .HasMaxLength(100);

            builder.Property(u => u.Lastname)
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationships (if applicable)
            builder.HasMany(u => u.UserRoles)
                   .WithOne(ur => ur.User)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
