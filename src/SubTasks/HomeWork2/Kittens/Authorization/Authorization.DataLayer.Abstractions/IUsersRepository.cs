using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authorization.DataLayer.Abstractions
{
    public interface IUsersRepository
    {
        Task<bool> AddUserAsync(string login, string password, IEnumerable<Claim> claims);
        Task<AuthInfo> FindUserAsync(string login, string password);
        Task<AuthInfo> FindUserByIdAsync(string id);
        Task UpdateUserRefreshTokenAsync(string userId, RefreshToken refreshToken);
    }
}