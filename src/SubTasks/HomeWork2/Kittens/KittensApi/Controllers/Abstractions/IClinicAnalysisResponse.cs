using KittensApi.Controllers.Responses;

namespace KittensApi.Controllers.Abstractions
{
    public interface IClinicAnalysisResponse
    {
        string Name { get; init; }
        bool Paid { get; init; }
        string Result { get; init; }
        public ClinicGetResponse Clinic { get; init; }
    }
}