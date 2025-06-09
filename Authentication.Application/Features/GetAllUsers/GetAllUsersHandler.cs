using Authentication.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Authentication.Application.Features.GetAllUsers
{
    public sealed class GetAllUsersHandler(IUserRepository _userRepository, IMapper _mapper): IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
    {
        public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var usersFromRepository = await _userRepository.GetAllUsers(request.CurrentUserId??0, request.Cursor, request.Limit, request.Search, cancellationToken);
            var users = _mapper.Map<List<GetAllUsersDto>>(usersFromRepository);
            return new GetAllUsersResponse
            {
                Users = users,
                NextCursor = users!=null ? users.LastOrDefault()?.CreatedAt : null,
            };
        }
    }
}
