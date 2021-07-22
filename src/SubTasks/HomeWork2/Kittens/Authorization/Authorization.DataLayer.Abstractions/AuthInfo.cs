using System.Collections.Generic;
using System.Security.Claims;

namespace Authorization.DataLayer.Abstractions
{
    public record AuthInfo(string Id, IReadOnlyCollection<Claim> Claims, RefreshToken LatestRefreshToken);
}