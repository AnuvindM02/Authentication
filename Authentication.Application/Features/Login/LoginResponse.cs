namespace Authentication.Application.Features.Login
{
    public class LoginResponse
    {
        public required string FirstName { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public required int UserId { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
