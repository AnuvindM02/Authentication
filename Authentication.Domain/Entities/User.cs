namespace Authentication.Domain.Entities
{
    public class User:BaseEntity
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public required string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
