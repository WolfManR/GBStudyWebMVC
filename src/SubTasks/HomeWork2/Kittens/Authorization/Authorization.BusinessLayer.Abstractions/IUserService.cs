namespace Authorization.BusinessLayer.Abstractions
{
    public interface IUserService
    {
        TokenData Authenticate(string user, string password);
        string RefreshToken(string token);
    }
}