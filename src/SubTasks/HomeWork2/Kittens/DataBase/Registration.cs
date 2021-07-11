using DataBase.Repository;
using DataLayer.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase
{
    public static class Registration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddTransient<IKittensRepository, KittensRepository>();
            return services;
        }
    }
}