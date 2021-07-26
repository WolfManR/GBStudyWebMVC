using System.Collections.Generic;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Controllers.Abstractions
{
    public interface IApiResponse
    {
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}