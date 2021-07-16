using DataLayer.Abstractions.Repositories;
using DataLayer.Maps;
using DataLayer.Repository;
using Mapster;

using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public static class Registration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddTransient<IKittensRepository, KittensRepository>();
            services.AddTransient<IClinicsRepository, ClinicsRepository>();
            TypeAdapterConfig.GlobalSettings.Apply(new KittensMaps(), new ClinicMaps());
            return services;
        }
    }
}