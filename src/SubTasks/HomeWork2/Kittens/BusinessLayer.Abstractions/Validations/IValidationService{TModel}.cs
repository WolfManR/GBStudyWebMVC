using System.Collections.Generic;

namespace BusinessLayer.Abstractions.Validations
{
    public interface IValidationService<in TModel> where TModel : class
    {
        IReadOnlyList<IOperationFailure> ValidateEntry(TModel entry);
    }
}