using MediatR;
namespace Authentication.Application.Features.GetAllUsers;
public sealed class GetAllUsersRequest : IRequest<GetAllUsersResponse>
{
    public int? CurrentUserId { get; set; }
    public DateTimeOffset? Cursor { get; set; }
    public int Limit { get; set; } = 10;
    public string? Search { get; set; } = null;
}
