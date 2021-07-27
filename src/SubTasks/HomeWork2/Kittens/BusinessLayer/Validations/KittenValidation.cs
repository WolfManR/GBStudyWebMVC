using BusinessLayer.Abstractions.Models;
using BusinessLayer.Abstractions.Validations;
using FluentValidation;

namespace BusinessLayer.Validations
{
    public class KittenValidation : ValidationService<Kitten>
    {
        public KittenValidation()
        {
            RuleFor(r => r.Nickname).NotNull().NotEmpty().WithMessage("Nickname cannot be empty").WithErrorCode("");
            RuleFor(r => r.Weight).GreaterThan(0).LessThan(40).WithMessage("Weight cannot be lower 0 or grater than 40").WithErrorCode("");
            RuleFor(r => r.Color).NotNull().NotEmpty().WithMessage("Kitten cannot be transparent").WithErrorCode("");
            RuleFor(r => r.Feed).NotNull().NotEmpty().WithMessage("Kitten must something to eat").WithErrorCode("");
        }
    }
}