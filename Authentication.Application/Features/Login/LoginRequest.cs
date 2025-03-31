using MediatR;

namespace Authentication.Application.Features.Login
{
    public class LoginRequest:IRequest<LoginResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ClientId { get; set; }
    }
}
