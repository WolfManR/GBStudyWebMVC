using System.Collections.Generic;
using BusinessLayer.Abstractions.Models.Analyzes;

namespace BusinessLayer.Abstractions.Models
{
    public class FullKittenCard
    {
        public Kitten Kitten { get; init; }
        public IReadOnlyCollection<Clinic> Clinics { get; init; }
        public IReadOnlyCollection<IClinicAnalysis> Analyzes { get; init; }
    }
}