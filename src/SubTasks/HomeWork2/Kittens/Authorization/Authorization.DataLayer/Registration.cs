using Authorization.DataLayer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.DataLayer
{
    public static class Registration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services) => services
            .AddTransient<IUsersRepository, UsersRepository>()
        ;
    }
}