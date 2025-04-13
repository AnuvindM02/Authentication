namespace Authentication.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public bool DisableSslValidation { get; set; }
    }
}
