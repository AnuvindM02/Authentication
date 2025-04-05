using System.Security.Claims;
using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using Authentication.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Authentication.Application.Features.UpdateProfile
{
    public class UpdateProfileHandler(IHttpContextAccessor _httpContextAccessor,
        IMapper _mapper,
        IUserRepository _userRepository,
        IPasswordHasher _passwordHasher) : IRequestHandler<UpdateProfileRequest, UpdateProfileResponse>
    {
        public async Task<UpdateProfileResponse> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
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

            if (await _userRepository.EmailExistsAsync(request.Email, user.Id, cancellationToken))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            string hashedPassword = _passwordHasher.HashPassword(request.Password);

            _mapper.Map(request, user);
            user.Password = hashedPassword;

            await _userRepository.UpdateAsync(user, cancellationToken);
            return _mapper.Map<UpdateProfileResponse>(user);
        }
    }
}
