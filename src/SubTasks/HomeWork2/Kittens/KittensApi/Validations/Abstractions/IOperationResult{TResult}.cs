using System.Collections.Generic;

namespace KittensApi.Validations.Abstractions
{
    public interface IOperationResult<TResult>
    {
        TResult Result { get; }
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}