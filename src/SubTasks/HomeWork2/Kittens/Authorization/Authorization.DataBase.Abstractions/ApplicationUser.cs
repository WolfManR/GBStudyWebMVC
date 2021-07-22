using Microsoft.AspNetCore.Identity;

using System;

namespace Authorization.DataBase.Abstractions
{
    public class ApplicationUser : IdentityUser
    {
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}