namespace Authentication.Application.Features.GetProfile
{
    public class GetProfileResponse
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public required List<string> Roles { get; set; }
    }
}
