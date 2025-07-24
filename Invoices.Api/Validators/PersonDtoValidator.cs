using FluentValidation;
using Invoices.Api.Models;

namespace Invoices.Api.Validators
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Mail)
                .EmailAddress().WithMessage("Invalid e-mail format.");

            RuleFor(x => x.Country)
                .IsInEnum().WithMessage("Invalid country value.");

            // A dále...
        }
    }
}