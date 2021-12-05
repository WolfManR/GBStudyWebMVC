using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;
using FluentValidation;

namespace BusinessLayer.Validations
{
    public class KittenValidation : ValidationService<Kitten>
    {
        public KittenValidation()
        {
            RuleFor(r => r.Nickname)
                .NotNull().WithMessage("Nickname cannot be empty").WithErrorCode("BCL-31a")
                .NotEmpty().WithMessage("Nickname cannot be empty").WithErrorCode("BCL-31b");
            RuleFor(r => r.Weight)
                .GreaterThan(0).WithMessage("Weight cannot be lower 0 or grater than 40").WithErrorCode("BCL-32a")
                .LessThan(40).WithMessage("Weight cannot be lower 0 or grater than 40").WithErrorCode("BCL-32b");
            RuleFor(r => r.Color)
                .NotNull().WithMessage("Kitten cannot be transparent").WithErrorCode("BCL-33a")
                .NotEmpty().WithMessage("Kitten cannot be transparent").WithErrorCode("BCL-33b");
            RuleFor(r => r.Feed)
                .NotNull().WithMessage("Kitten must something to eat").WithErrorCode("BCL-34a")
                .NotEmpty().WithMessage("Kitten must something to eat").WithErrorCode("BCL-34b");
        }
    }
}