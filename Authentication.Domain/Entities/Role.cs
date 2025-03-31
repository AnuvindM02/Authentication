﻿namespace Authentication.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
