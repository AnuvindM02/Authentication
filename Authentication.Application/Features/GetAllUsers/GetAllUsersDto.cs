namespace Authentication.Application.Features.GetAllUsers
{
    public class GetAllUsersDto
    {
        public required int UserId { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
    }
}
