using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class UserRole
    {
        // Foreign key referencing User.
        public int UserId { get; set; }
        // Navigation property to User.
        public required User User { get; set; }
        // Foreign key referencing Role.
        public int RoleId { get; set; }
        // Navigation property to Role.
        public required Role Role { get; set; }
    }
}
