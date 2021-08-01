namespace DataLayer.Abstractions.Entities
{
    public interface IClinicAnalysis : IAnalysis
    {
        Clinic Clinic { get; init; }
    }
}