using System.Threading.Tasks;

namespace Authorization.BusinessLayer.Abstractions
{
    public interface IUserService
    {
        Task<TokenData> Authenticate(string user, string password);
        Task<bool> RegisterUser(string login, string password);
        Task<string> UpdateRefreshToken(string token);
    }
}