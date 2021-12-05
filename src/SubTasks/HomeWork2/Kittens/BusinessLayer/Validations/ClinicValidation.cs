using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;

using FluentValidation;

namespace BusinessLayer.Validations
{
    public class ClinicValidation : ValidationService<Clinic>
    {
        public ClinicValidation()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Clinic name cannot be empty").WithErrorCode("BCL-41a")
                .NotEmpty().WithMessage("Clinic name cannot be empty").WithErrorCode("BCL-41b");
        }
    }
}