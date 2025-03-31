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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Table name (optional)
            builder.ToTable("Clients");

            // Set Primary Key
            builder.HasKey(c => c.Id);

            // Unique constraint for ClientId
            builder.HasIndex(c => c.ClientId)
                   .IsUnique();

            // Property Configurations
            builder.Property(c => c.ClientId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.ClientURL)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasData(
                new Client
                {
                    Id = 1,
                    ClientId = "Client1",
                    Name = "Client Application 1",
                    ClientURL = "https://client1.com"
                },
                new Client
                {
                    Id = 2,
                    ClientId = "Client2",
                    Name = "Client Application 2",
                    ClientURL = "https://client2.com"
                }
            );
        }
    }
}
