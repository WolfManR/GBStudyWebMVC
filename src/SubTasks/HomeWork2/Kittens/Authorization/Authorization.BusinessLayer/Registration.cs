using Authorization.BusinessLayer.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.BusinessLayer
{
    public static class Registration
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services) => services
            .AddTransient<IUserService, UserService>()
            ;
    }
}