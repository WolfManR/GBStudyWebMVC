using DataLayer.Abstractions.Entities;
using Data = DataBase.Abstractions.Entities;
using Mapster;

namespace DataLayer.Maps
{
    public class AnalysisMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Data::Analyzes.Analysis, IAnalysis>()
                .Include<Data::Analyzes.Analysis, IClinicAnalysis>()
                .Include<Data::Analyzes.BloodAnalysis, BloodAnalysis>()
                .Include<Data::Analyzes.InspectionAnalysis, InspectionAnalysis>()
                .TwoWays();
        }
    }
}