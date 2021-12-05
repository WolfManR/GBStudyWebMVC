namespace KittensApi.Controllers.Abstractions
{
    public interface IApiResponse<out TResult> : IApiResponse
    {
        TResult Result { get; }
    }
}