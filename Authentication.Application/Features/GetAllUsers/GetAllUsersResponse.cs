namespace Authentication.Application.Features.GetAllUsers
{
    public class GetAllUsersResponse
    {
        public List<GetAllUsersDto>? Users { get; set; }
        public DateTimeOffset? NextCursor { get; set; }
    }
}
