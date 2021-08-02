using BusinessLayer.Abstractions.Models.Analyzes;
using Mapster;
using Data = DataLayer.Abstractions.Entities;

namespace BusinessLayer.Maps
{
    public class AnalysisMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Data::IAnalysis, IAnalysis>()
                .Include<Data::IClinicAnalysis, IClinicAnalysis>()
                .Include<Data::BloodAnalysis, BloodAnalysis>()
                .Include<Data::InspectionAnalysis, InspectionAnalysis>()
                .TwoWays();
        }
    }
}