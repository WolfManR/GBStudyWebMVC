namespace DataLayer.Abstractions.Entities
{
    public interface IAnalysis
    {
        int Id { get; init; }
        string Name { get; init; }
        bool Paid { get; init; }
        string Result { get; init; }
    }
}