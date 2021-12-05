using Authorization.DataBase.Abstractions;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.DataBase
{
    public class AuthorizationContext : IdentityDbContext<ApplicationUser>
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options) : base(options)
        {

        }
    }
}