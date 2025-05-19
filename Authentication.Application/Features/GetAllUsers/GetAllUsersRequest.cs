using MediatR;

namespace Authentication.Application.Features.GetAllUsers;
    public sealed record GetAllUsersRequest(DateTimeOffset? cursor, int limit, string? search) :IRequest<GetAllUsersResponse>;
