namespace Authentication.Application.Features.UpdateProfile
{
    public class UpdateProfileResponse
    {
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
    }
}