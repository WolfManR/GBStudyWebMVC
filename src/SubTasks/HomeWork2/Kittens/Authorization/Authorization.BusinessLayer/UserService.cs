using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System;
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
                
            RefreshToken refreshToken = GenerateRefreshToken(authInfo.Id);
            await _repository.UpdateUserRefreshTokenAsync(authInfo.Id, _mapper.Map<DataRefreshToken>(refreshToken));
            return new TokenData()
            {
                Token = GenerateJwtToken(authInfo.Id, 15),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<bool> Register(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return await _repository.AddUserAsync(login, password);
        }

        public async Task<string> UpdateRefreshToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            if (tokenHandler.CanReadToken(token))
            {
                var security = tokenHandler.ReadJwtToken(token);
                var idClaim = security.Claims.SingleOrDefault(c => c.ValueType == ClaimTypes.NameIdentifier);
                if (idClaim is null) return string.Empty;
                var userId = idClaim.Value;
                var dataRefreshToken = await _repository.GetUserRefreshTokenAsync(userId);
                var refreshToken = _mapper.Map<RefreshToken>(dataRefreshToken);
                if (string.CompareOrdinal(refreshToken.Token, token) == 0 && !refreshToken.IsExpired)
                {
                    refreshToken = GenerateRefreshToken(userId);
                    await _repository.UpdateUserRefreshTokenAsync(userId, _mapper.Map<DataRefreshToken>(refreshToken));
                    return refreshToken.Token;
                }
            }
            
            return string.Empty;
        }

        private string GenerateJwtToken(string id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.SecureCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string id)
        {
            RefreshToken refreshToken = new RefreshToken
            {
                Expires = DateTime.Now.AddMinutes(360), Token = GenerateJwtToken(id, 360)
            };
            return refreshToken;
        }

        
    }
}