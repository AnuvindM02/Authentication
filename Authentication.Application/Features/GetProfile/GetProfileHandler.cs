using System.Security.Claims;
using Authentication.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace Authentication.Application.Features.GetProfile
{
    public class GetProfileHandler(IUserRepository _userRepository,
        IHttpContextAccessor _httpContextAccessor,
        IMapper _mapper) : IRequestHandler<GetProfileRequest, GetProfileResponse>
    {
        public async Task<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new UnauthorizedAccessException("No HTTP context available");
            }

            var emailClaim = httpContext.User?.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
            {
                throw new UnauthorizedAccessException("Invalid token: Email claim missing.");
            }

            string userEmail = emailClaim.Value;

            var user = await _userRepository.GetByEmailAsync(userEmail, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<GetProfileResponse>(user);
        }
    }
}
