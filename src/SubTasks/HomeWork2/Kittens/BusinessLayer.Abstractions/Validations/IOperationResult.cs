using System.Collections.Generic;

namespace BusinessLayer.Abstractions.Validations
{
    public interface IOperationResult
    {
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}