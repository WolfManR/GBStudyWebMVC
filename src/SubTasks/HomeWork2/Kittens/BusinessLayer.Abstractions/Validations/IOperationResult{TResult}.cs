namespace BusinessLayer.Abstractions.Validations
{
    public interface IOperationResult<out TResult> : IOperationResult
    {
        TResult Result { get; }
    }
}