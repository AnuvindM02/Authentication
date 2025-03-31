using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class SigningKey
    {
        [Key]
        public int Id { get; set; }
        // Unique identifier for the key (Key ID).
        [Required]
        [MaxLength(100)]
        public required string KeyId { get; set; }
        // The RSA private key.
        [Required]
        public required string PrivateKey { get; set; }
        // The RSA public key in XML or PEM format.
        [Required]
        public required string PublicKey { get; set; }
        // Indicates if the key is active.
        [Required]
        public required bool IsActive { get; set; }
        // Date when the key was created.
        [Required]
        public required DateTime CreatedAt { get; set; }
        // Date when the key is set to expire.
        [Required]
        public required DateTime ExpiresAt { get; set; }
    }
}
