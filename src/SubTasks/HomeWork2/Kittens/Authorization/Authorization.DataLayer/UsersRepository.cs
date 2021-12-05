using System.Collections.Generic;
using Authorization.DataLayer.Abstractions;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Threading.Tasks;
using Authorization.DataBase.Abstractions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using RefreshToken = Authorization.DataLayer.Abstractions.RefreshToken;

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

        public async Task<bool> AddUserAsync(string login, string password, IEnumerable<Claim> claims)
        {
            var user = new ApplicationUser()
            {
                UserName = login,
                Email = login
            };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return false;

            var assignResult = await AssignClaims(user, claims);

            return assignResult;
        }

        private async Task<bool> AssignClaims(ApplicationUser user, IEnumerable<Claim> claims)
        {
            var result = await _userManager.AddClaimsAsync(user, claims);
            return result.Succeeded;
        }

        public async Task<AuthInfo> FindUserAsync(string login, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(login, password, false, false);
            if (!result.Succeeded) return null;

            var user = await _userManager.FindByEmailAsync(login);
            if (user is null) return null;
            var claims = await _userManager.GetClaimsAsync(user);
            return new AuthInfo(user.Id, claims.ToImmutableList(), _mapper.Map<RefreshToken>(user));
        }

        public async Task<AuthInfo> FindUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return null;
            var claims = await _userManager.GetClaimsAsync(user);
            return new AuthInfo(user.Id, claims.ToImmutableList(), _mapper.Map<RefreshToken>(user));
        }

        public async Task UpdateUserRefreshTokenAsync(string userId, RefreshToken refreshToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return;
            user.Token = refreshToken.Token;
            user.TokenExpires = refreshToken.Expires;
            await _userManager.UpdateAsync(user);
        }
    }
}