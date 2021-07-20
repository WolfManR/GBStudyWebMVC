using System;

namespace Authorization.DataBase.Abstractions
{
    public sealed class RefreshToken
    {
        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}