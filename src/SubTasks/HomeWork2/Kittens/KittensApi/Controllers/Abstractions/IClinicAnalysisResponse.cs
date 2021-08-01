using KittensApi.Controllers.Responses;

namespace KittensApi.Controllers.Abstractions
{
    public interface IClinicAnalysisResponse
    {
        public ClinicGetResponse Clinic { get; init; }
    }
}