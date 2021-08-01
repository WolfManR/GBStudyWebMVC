using System.Collections.Generic;
using KittensApi.Controllers.Abstractions;

namespace KittensApi.Controllers.Responses
{
    public class KittenFullCardResponse
    {
        public KittenGetResponse Kitten { get; init; }
        public IEnumerable<ClinicGetResponse> Clinic { get; init; }
        public IEnumerable<IClinicAnalysisResponse> Analyzes { get; init; }
    }
}