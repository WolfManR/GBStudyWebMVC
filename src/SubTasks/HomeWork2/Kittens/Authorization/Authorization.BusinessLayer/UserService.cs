using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Authorization.BusinessLayer.Abstractions;
using Authorization.DataLayer.Abstractions;
using MapsterMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RefreshToken = Authorization.BusinessLayer.Abstractions.RefreshToken;
using DataRefreshToken = Authorization.DataLayer.Abstractions.RefreshToken;

namespace Authorization.BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        
        public UserService(IOptions<JwtSettings> jwtSettings, IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<TokenData> Authenticate(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var authInfo = await _repository.FindUserAsync(login, password);
            if (authInfo is null) return null;
                
            RefreshToken refreshToken = GenerateRefreshToken(PrepareUserClaims(authInfo, _jwtSettings.ValidIssuer, _jwtSettings.ValidAudience));
            await _repository.UpdateUserRefreshTokenAsync(authInfo.Id, _mapper.Map<DataRefreshToken>(refreshToken));
            return new TokenData()
            {
                Token = GenerateJwtToken(PrepareUserClaims(authInfo, _jwtSettings.ValidIssuer, _jwtSettings.ValidAudience), 15),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<bool> RegisterUser(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "User")
            };
            return await _repository.AddUserAsync(login, password, claims);
        }

        public async Task<string> UpdateRefreshToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            if (tokenHandler.CanReadToken(token))
            {
                var security = tokenHandler.ReadJwtToken(token);
                var idClaim = security.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
                if (idClaim is null) return string.Empty;
                var userId = idClaim.Value;
                var authInfo = await _repository.FindUserByIdAsync(userId);
                if(authInfo is null) return string.Empty;
                var refreshToken = _mapper.Map<RefreshToken>(authInfo.LatestRefreshToken);
                if (string.CompareOrdinal(refreshToken.Token, token) == 0 && !refreshToken.IsExpired)
                {
                    refreshToken = GenerateRefreshToken(PrepareUserClaims(authInfo, _jwtSettings.ValidIssuer, _jwtSettings.ValidAudience));
                    await _repository.UpdateUserRefreshTokenAsync(userId, _mapper.Map<DataRefreshToken>(refreshToken));
                    return refreshToken.Token;
                }
            }
            
            return string.Empty;
        }

        private IEnumerable<Claim> PrepareUserClaims(AuthInfo authInfo, string issuer, string audience)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.NameId, authInfo.Id),
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience)
            };
            var roles = authInfo.Claims.Where(c => c.Type == ClaimTypes.Role);
            foreach (var role in roles)
            {
                claims.Add(role);
            }
            return claims;
        }

        private string GenerateJwtToken(IEnumerable<Claim> userClaims, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.SecureCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(IEnumerable<Claim> userClaims)
        {
            RefreshToken refreshToken = new RefreshToken
            {
                Expires = DateTime.Now.AddMinutes(360), Token = GenerateJwtToken(userClaims, 360)
            };
            return refreshToken;
        }
    }
}