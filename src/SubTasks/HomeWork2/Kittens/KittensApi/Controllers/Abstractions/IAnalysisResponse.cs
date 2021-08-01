namespace KittensApi.Controllers.Abstractions
{
    public interface IAnalysisResponse
    {
        string Name { get; init; }
        bool Paid { get; init; }
        string Result { get; init; }
    }
}