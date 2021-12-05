using System.Collections.Generic;
using BusinessLayer.Abstractions.Models.Analyzes;

namespace BusinessLayer.Abstractions.Models
{
    public class KittenCard
    {
        public Kitten Kitten { get; init; }
        public Clinic Clinic { get; init; }
        public IReadOnlyCollection<IAnalysis> Analyzes { get; init; }
    }
}