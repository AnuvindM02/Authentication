﻿using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using Authentication.Domain.Entities;
using Authentication.Domain.Enums;
using AutoMapper;
using MediatR;
namespace Authentication.Application.Features.CreateProfile
{
    public sealed class CreateProfileHandler(IUserRepository _userRepository,
        IRoleRepository _roleRepository,
        IPasswordHasher _passwordHasher,
        IUserRoleRepository _userRoleRepository,
        IMapper _mapper) : IRequestHandler<CreateProfileRequest, CreateProfileResponse>
    {
        public async Task<CreateProfileResponse> Handle(CreateProfileRequest request, CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailExistsAsync(request.Email, 0, cancellationToken))
            {
                throw new InvalidOperationException("Email is already registered");
            }

            string hashedPassword = _passwordHasher.HashPassword(request.Password);

            var newUser = _mapper.Map<User>(request);
            newUser.Password = hashedPassword;

            await _userRepository.AddAsync(newUser, cancellationToken);

            var userRole = await _roleRepository.GetByConditionAsync(x => x.Id == (int)RoleType.User);

            if (userRole != null)
            {
                var newUserRole = new UserRole
                {
                    Role = userRole,
                    User = newUser,
                    RoleId = userRole.Id,
                    UserId = newUser.Id
                };
                
                await _userRoleRepository.AddAsync(newUserRole, cancellationToken);
            }

            return _mapper.Map<CreateProfileResponse>(newUser);
        }
    }
}
