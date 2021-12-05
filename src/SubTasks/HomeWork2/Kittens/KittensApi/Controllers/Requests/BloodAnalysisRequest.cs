using KittensApi.Controllers.Abstractions;

namespace KittensApi.Controllers.Requests
{
    public class BloodAnalysisRequest : IAnalysisRequest
    {
        public string Name { get; init; }
        public bool Paid { get; init; }
        public string Result { get; init; }
    }
}