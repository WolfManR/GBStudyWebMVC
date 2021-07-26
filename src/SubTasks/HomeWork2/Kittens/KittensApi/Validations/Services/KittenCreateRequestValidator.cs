using FluentValidation;
using KittensApi.Controllers.Requests;
using KittensApi.Validations.Abstractions;

namespace KittensApi.Validations.Services
{
    public class KittenCreateRequestValidator : ValidationService<KittenCreateRequest>
    {
        public KittenCreateRequestValidator()
        {
            RuleFor(r => r.Nickname).NotNull().NotEmpty().WithMessage("Nickname cannot be empty").WithErrorCode("");
            RuleFor(r => r.Weight).GreaterThan(0).LessThan(40).WithMessage("Weight cannot be lower 0 or grater than 40").WithErrorCode("");
            RuleFor(r => r.Color).NotNull().NotEmpty().WithMessage("Kitten cannot be transparent").WithErrorCode("");
            RuleFor(r => r.Feed).NotNull().NotEmpty().WithMessage("Kitten must something to eat").WithErrorCode("");
        }
    }
}