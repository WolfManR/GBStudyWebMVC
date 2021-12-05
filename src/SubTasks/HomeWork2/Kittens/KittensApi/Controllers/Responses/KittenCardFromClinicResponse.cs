using System.Collections.Generic;
using KittensApi.Controllers.Abstractions;

namespace KittensApi.Controllers.Responses
{
    public class KittenCardFromClinicResponse
    {
        public KittenGetResponse Kitten { get; init; }
        public ClinicGetResponse Clinic { get; init; }
        public IEnumerable<IAnalysisResponse> Analyzes { get; init; }
    }
}