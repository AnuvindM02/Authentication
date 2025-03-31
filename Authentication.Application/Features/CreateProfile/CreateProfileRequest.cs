using MediatR;

namespace Authentication.Application.Features.CreateProfile
{
    public class CreateProfileRequest:IRequest<CreateProfileResponse>
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
