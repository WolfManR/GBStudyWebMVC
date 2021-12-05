namespace BusinessLayer.Abstractions.Models.Analyzes
{
    public interface IClinicAnalysis : IAnalysis
    {
        Clinic Clinic { get; init; }
    }
}