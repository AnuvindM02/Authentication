using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using MediatR;
namespace Authentication.Application.Features.Login
{
    public sealed class LoginHandler(IClientRepository _clientRepository,
        IUserRepository _userRepository,
        IPasswordHasher _passwordHasher,
        IJwtTokenService _jwtTokenService) : IRequestHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByClientIdAsync(request.ClientId, cancellationToken)??
                throw new UnauthorizedAccessException("Invalid client credentials");

            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)??
                throw new UnauthorizedAccessException("Invalid credentials");

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.Password);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid credentials");

            //Authentication Successful, Generate JWT
            var token = await _jwtTokenService.GenerateToken(user, client, cancellationToken);
            return new LoginResponse { RefreshToken = "", Token = token, UserId = user.Id, FirstName = user.FirstName, Middlename = user.Middlename,  Lastname = user.Lastname };
        }

    }
}
