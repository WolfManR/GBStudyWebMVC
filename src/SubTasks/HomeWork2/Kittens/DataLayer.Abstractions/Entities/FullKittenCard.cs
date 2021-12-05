using System.Collections.Generic;

namespace DataLayer.Abstractions.Entities
{
    public class FullKittenCard
    {
        public Kitten Kitten { get; init; }
        public IReadOnlyCollection<Clinic> Clinics { get; init; }
        public IReadOnlyCollection<IClinicAnalysis> Analyzes { get; init; }
    }
}