using Authorization.DataLayer.Abstractions;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Threading.Tasks;
using Authorization.DataBase.Abstractions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using RefreshToken = Authorization.DataLayer.Abstractions.RefreshToken;
using DataRefreshToken = Authorization.DataBase.Abstractions.RefreshToken;

namespace Authorization.DataLayer
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UsersRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> AddUserAsync(string login, string password)
        {
            var user = new ApplicationUser()
            {
                UserName = login,
                Email = login
            };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return false;

            var roleResult = await _userManager.AddClaimsAsync(user,new []{new Claim("Id", user.Id), new Claim("Role", "User")});
            if (!roleResult.Succeeded) return false;

            return true;
        }

        public async Task<AuthInfo> FindUserAsync(string login, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(login, password, false, false);
            if (!result.Succeeded) return null;

            var user = await _userManager.FindByEmailAsync(login);
            if (user is null) return null;
            var claims = await _userManager.GetClaimsAsync(user);
            return new AuthInfo(user.Id, claims.ToImmutableList(), _mapper.Map<RefreshToken>(user.LatestRefreshToken));
        }

        public async Task<RefreshToken> GetUserRefreshTokenAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<RefreshToken>(user.LatestRefreshToken);
        }

        public async Task UpdateUserRefreshTokenAsync(string userId, RefreshToken refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return;
            user.LatestRefreshToken = _mapper.Map<DataRefreshToken>(refreshToken);
            await _userManager.UpdateAsync(user);
        }
    }
}