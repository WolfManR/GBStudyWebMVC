using BusinessLayer.Abstractions.Models.Analyzes;
using KittensApi.Controllers.Abstractions;
using KittensApi.Controllers.Requests;
using KittensApi.Controllers.Responses;
using Mapster;

namespace KittensApi.Maps
{
    public class AnalysisMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<IAnalysisRequest, IAnalysis>()
                .Include<BloodAnalysisRequest, BloodAnalysis>()
                .Include<InspectionAnalysisRequest, InspectionAnalysis>();

            config.ForType<IAnalysis, IAnalysisResponse>()
                .Include<BloodAnalysis, BloodAnalysisGetResponse>()
                .Include<InspectionAnalysis, InspectionAnalysisGetResponse>();

            config.ForType<IClinicAnalysis, IClinicAnalysisResponse>()
                .Include<BloodAnalysis, BloodAnalysisGetResponse>()
                .Include<InspectionAnalysis, InspectionAnalysisGetResponse>();
        }
    }
}