using BusinessLayer.Abstractions.Services;
using BusinessLayer.Maps;
using BusinessLayer.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer
{
    public static class Registration
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddTransient<IKittensService, KittensService>();
            services.AddTransient<IClinicsService, ClinicsService>();
            TypeAdapterConfig.GlobalSettings.Apply(new KittensMaps(), new ClinicsMaps());
            return services;
        }
    }
}