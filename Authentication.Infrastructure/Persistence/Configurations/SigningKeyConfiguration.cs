using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Persistence.Configurations
{
    public class SigningKeyConfiguration : IEntityTypeConfiguration<SigningKey>
    {
        public void Configure(EntityTypeBuilder<SigningKey> builder)
        {
            builder.ToTable("SigningKeys");
            // Set Primary Key
            builder.HasKey(sk => sk.Id);

            // Unique constraint for KeyId
            builder.HasIndex(sk => sk.KeyId)
                   .IsUnique();

            // Property Configurations
            builder.Property(sk => sk.KeyId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(sk => sk.PrivateKey)
                   .IsRequired();

            builder.Property(sk => sk.PublicKey)
                   .IsRequired();

            builder.Property(sk => sk.IsActive)
                   .IsRequired();

            builder.Property(sk => sk.CreatedAt)
                   .IsRequired();

            builder.Property(sk => sk.ExpiresAt)
                   .IsRequired();
        }
    }
}
