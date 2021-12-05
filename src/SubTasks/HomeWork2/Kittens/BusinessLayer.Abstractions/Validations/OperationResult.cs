using System.Collections.Generic;

namespace BusinessLayer.Abstractions.Validations
{
    public class OperationResult : IOperationResult
    {
        public OperationResult(IReadOnlyList<IOperationFailure> failures, bool succeed)
        {
            Failures = failures;
            Succeed = succeed;
        }

        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }
    }
}