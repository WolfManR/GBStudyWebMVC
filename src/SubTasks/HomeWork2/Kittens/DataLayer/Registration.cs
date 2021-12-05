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
            services.AddTransient<IAnalysisRepository, AnalysisRepository>();
            services.AddTransient<IKittenCardsRepository, KittensRepository>();
            TypeAdapterConfig.GlobalSettings.Apply(new KittensMaps(), new ClinicMaps(), new AnalysisMaps());
            return services;
        }
    }
}