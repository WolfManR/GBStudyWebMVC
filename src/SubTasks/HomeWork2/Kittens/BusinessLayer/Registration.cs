using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Services;
using BusinessLayer.Abstractions.Validations;
using BusinessLayer.Maps;
using BusinessLayer.Services;
using BusinessLayer.Validations;
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
            services.AddSingleton<IValidationService<Clinic>, ClinicValidation>();
            services.AddSingleton<IValidationService<Kitten>, KittenValidation>();
            TypeAdapterConfig.GlobalSettings.Apply(new KittensMaps(), new ClinicsMaps());
            return services;
        }
    }
}