using KittensApi.Controllers.Responses;

namespace KittensApi.Controllers.Abstractions
{
    public interface IClinicAnalysisResponse : IAnalysisResponse
    {
        public ClinicGetResponse Clinic { get; init; }
    }
}