namespace Authorization.BusinessLayer.Abstractions
{
    public sealed class TokenData
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}