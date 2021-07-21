using System.Threading.Tasks;

namespace Authorization.DataLayer.Abstractions
{
    public interface IUsersRepository
    {
        Task<bool> AddUserAsync(string login, string password);
        Task<AuthInfo> FindUserAsync(string login, string password);
        Task<RefreshToken> GetUserRefreshTokenAsync(string id);
        Task UpdateUserRefreshTokenAsync(string userId, RefreshToken refreshToken);
    }
}