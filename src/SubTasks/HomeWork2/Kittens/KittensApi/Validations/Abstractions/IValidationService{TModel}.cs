using System.Collections.Generic;

namespace KittensApi.Validations.Abstractions
{
    public interface IValidationService<in TModel> where TModel : class
    {
        IReadOnlyList<IOperationFailure> ValidateEntry(TModel entry);
    }
}