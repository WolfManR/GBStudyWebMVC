namespace AuthorizationApi.Controllers.Responses
{
    public sealed class AuthenticateResponse
    {
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}