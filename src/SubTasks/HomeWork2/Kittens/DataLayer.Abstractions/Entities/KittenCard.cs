using System.Collections.Generic;

namespace DataLayer.Abstractions.Entities
{
    public class KittenCard
    {
        public Kitten Kitten { get; init; }
        public Clinic Clinic { get; init; }
        public IReadOnlyCollection<IAnalysis> Analyzes { get; init; }
    }
}