using Microsoft.AspNetCore.Identity;

namespace Authorization.DataBase.Abstractions
{
    public class ApplicationUser : IdentityUser
    {
        public RefreshToken LatestRefreshToken { get; set; }
    }
}