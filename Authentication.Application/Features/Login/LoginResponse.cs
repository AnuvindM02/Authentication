namespace Authentication.Application.Features.Login
{
    public class LoginResponse
    {
        public required int UserId { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
