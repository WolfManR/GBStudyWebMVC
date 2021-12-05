using System.Collections.Generic;
using KittensApi.Controllers.Abstractions;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Controllers.Responses
{
    public class KittenUpdateResponse : IApiResponse
    {
        public KittenUpdateResponse(IReadOnlyList<IOperationFailure> failures, bool succeed)
        {
            Failures = failures;
            Succeed = succeed;
        }

        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }
    }
}