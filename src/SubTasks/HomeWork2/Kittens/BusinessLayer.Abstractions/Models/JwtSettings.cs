namespace BusinessLayer.Abstractions.Models
{
    public class JwtSettings
    {
        public string SecureCode { get; init; }
        public string ValidAudience { get; init; }
        public string ValidIssuer { get; init; }
    }
}