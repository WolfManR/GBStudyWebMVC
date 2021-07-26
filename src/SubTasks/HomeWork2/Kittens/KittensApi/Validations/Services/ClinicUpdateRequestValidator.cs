using FluentValidation;
using KittensApi.Controllers.Requests;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Validations.Services
{
    public class ClinicUpdateRequestValidator : ValidationService<ClinicUpdateRequest>
    {
        public ClinicUpdateRequestValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Clinic name cannot be empty").WithErrorCode("");
        }
    }
}