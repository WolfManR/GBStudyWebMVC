using System.Collections.Generic;
using KittensApi.Controllers.Abstractions;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Controllers.Responses
{
    public class ClinicUpdateResponse : IApiResponse
    {
        public ClinicUpdateResponse(IReadOnlyList<IOperationFailure> failures, bool succeed)
        {
            Failures = failures;
            Succeed = succeed;
        }

        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }
    }
}