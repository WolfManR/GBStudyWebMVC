namespace KittensApi.Controllers.Abstractions
{
    public interface IAnalysisRequest
    {
        string Name { get; init; }
        bool Paid { get; init; }
        string Result { get; init; }
    }
}