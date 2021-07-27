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
                .NotNull().WithMessage("Nickname cannot be empty").WithErrorCode("BCL-31")
                .NotEmpty().WithMessage("Nickname cannot be empty").WithErrorCode("BCL-31");
            RuleFor(r => r.Weight)
                .GreaterThan(0).WithMessage("Weight cannot be lower 0 or grater than 40").WithErrorCode("BCL-32")
                .LessThan(40).WithMessage("Weight cannot be lower 0 or grater than 40").WithErrorCode("BCL-32");
            RuleFor(r => r.Color)
                .NotNull().WithMessage("Kitten cannot be transparent").WithErrorCode("BCL-33")
                .NotEmpty().WithMessage("Kitten cannot be transparent").WithErrorCode("BCL-33");
            RuleFor(r => r.Feed)
                .NotNull().WithMessage("Kitten must something to eat").WithErrorCode("BCL-34")
                .NotEmpty().WithMessage("Kitten must something to eat").WithErrorCode("BCL-34");
        }
    }
}