namespace Authentication.Application.RabbitMq.Models
{
    public sealed class NewUserMessage
    {
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
    }
}
