using Authentication.Application.Interfaces.Repositories;
using Authentication.Application.Interfaces.Services;
using Authentication.Application.RabbitMq;
using Authentication.Application.RabbitMq.Models;
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
        IMapper _mapper,
        IRabbitMqPublisher _rabbitMqPublisher,
        IClientRepository _clientRepository,
        IJwtTokenService _jwtTokenService) : IRequestHandler<CreateProfileRequest, CreateProfileResponse>
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

                //Rabbitmq publish
                string routingKey = "auth.create.user";
                var message = _mapper.Map<NewUserMessage>(newUser);
                _rabbitMqPublisher.Publish(routingKey, message);
            }

            //Get the token with created user
            var client = await _clientRepository.GetFirst(cancellationToken)??
                throw new KeyNotFoundException("No client configured");
            var token = await _jwtTokenService.GenerateToken(newUser, client, cancellationToken);

            var response = _mapper.Map<CreateProfileResponse>(newUser);
            response.Token = token;
            return response;
        }
    }
}
