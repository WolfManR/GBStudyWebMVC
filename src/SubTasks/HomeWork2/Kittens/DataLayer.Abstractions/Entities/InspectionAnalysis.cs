namespace DataLayer.Abstractions.Entities
{
    public class InspectionAnalysis : IClinicAnalysis
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public bool Paid { get; init; }
        public string Result { get; init; }
        public Clinic Clinic { get; init; }
    }
}