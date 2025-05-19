namespace Authentication.Domain.Entities
{
    public class BaseEntity
    {
        public required DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
