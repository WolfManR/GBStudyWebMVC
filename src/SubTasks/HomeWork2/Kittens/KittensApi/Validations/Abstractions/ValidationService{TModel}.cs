using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace KittensApi.Validations.Abstractions
{
    public abstract class ValidationService<TModel> : AbstractValidator<TModel>, IValidationService<TModel> where TModel : class
    {
        public IReadOnlyList<IOperationFailure> ValidateEntry(TModel entry)
        {
            ValidationResult result = Validate(entry);

            if (result is null || result.Errors.Count == 0)
            {
                return ArraySegment<IOperationFailure>.Empty;
            }

            List<IOperationFailure> failures = new(result.Errors.Count);

            foreach (ValidationFailure error in result.Errors)
            {
                failures.Add(new OperationFailure()
                {
                    PropertyName = error.PropertyName,
                    Description = error.ErrorMessage,
                    Code = error.ErrorCode
                });
            }

            return failures;

        }
    }
}