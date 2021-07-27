using FluentValidation;
using KittensApi.Controllers.Requests;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Validations.Services
{
    public class ClinicCreateRequestValidator : ValidationService<ClinicCreateRequest>
    {
        public ClinicCreateRequestValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Clinic name cannot be empty").WithErrorCode("BCL-41")
                .NotEmpty().WithMessage("Clinic name cannot be empty").WithErrorCode("BCL-41");
        }
    }
}