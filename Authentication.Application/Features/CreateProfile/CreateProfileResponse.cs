namespace Authentication.Application.Features.CreateProfile
{
    public class CreateProfileResponse
    {
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
