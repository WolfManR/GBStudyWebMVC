using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Authorization.BusinessLayer.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IDictionary<string, AuthInfo> _users = new Dictionary<string, AuthInfo>()
        {
            {"test", new AuthInfo("test")}
        };

        public UserService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public TokenData Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            int i = 0;
            foreach (KeyValuePair<string, AuthInfo> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Key, user) == 0 && string.CompareOrdinal(pair.Value.Password, password) == 0)
                {
                    RefreshToken refreshToken = GenerateRefreshToken(i);
                    pair.Value.LatestRefreshToken = refreshToken;
                    return new TokenData()
                    {
                        Token = GenerateJwtToken(i, 15),
                        RefreshToken = refreshToken.Token
                    };
                }
            }
            return null;
        }

        public string RefreshToken(string token)
        {
            int i = 0;
            foreach (KeyValuePair<string, AuthInfo> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Value.LatestRefreshToken.Token, token) == 0
                    && pair.Value.LatestRefreshToken.IsExpired is false)
                {
                    pair.Value.LatestRefreshToken = GenerateRefreshToken(i);
                    return pair.Value.LatestRefreshToken.Token;
                }
            }
            return string.Empty;
        }

        private string GenerateJwtToken(int id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.SecureCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(int id)
        {
            RefreshToken refreshToken = new RefreshToken
            {
                Expires = DateTime.Now.AddMinutes(360), Token = GenerateJwtToken(id, 360)
            };
            return refreshToken;
        }

        private record AuthInfo(string Password)
        {
            public RefreshToken LatestRefreshToken { get; set; }
        };
    }
}