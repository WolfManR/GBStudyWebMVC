using KittensApi.Controllers.Abstractions;

namespace KittensApi.Controllers.Responses
{
    public class InspectionAnalysisGetResponse : IClinicAnalysisResponse, IAnalysisResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public bool Paid { get; init; }
        public string Result { get; init; }
        public ClinicGetResponse Clinic { get; init; }
    }
}