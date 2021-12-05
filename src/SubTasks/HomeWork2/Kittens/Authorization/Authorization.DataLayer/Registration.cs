using Authorization.DataBase.Abstractions;
using Authorization.DataLayer.Abstractions;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.DataLayer
{
    public static class Registration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.ForType<ApplicationUser, RefreshToken>()
                .Map(dest => dest.Expires, src => src.TokenExpires);
            return services
                .AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}