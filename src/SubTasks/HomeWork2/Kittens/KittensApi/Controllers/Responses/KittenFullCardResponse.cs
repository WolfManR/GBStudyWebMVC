using System.Collections.Generic;
using KittensApi.Controllers.Abstractions;

namespace KittensApi.Controllers.Responses
{
    public class KittenFullCardResponse
    {
        public KittenGetResponse Kitten { get; init; }
        public IReadOnlyCollection<ClinicGetResponse> Clinics { get; init; }
        public IReadOnlyCollection<IClinicAnalysisResponse> Analyzes { get; init; }
    }
}