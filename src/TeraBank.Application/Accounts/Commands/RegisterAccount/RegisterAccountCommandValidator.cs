using FluentValidation;
using FluentValidation.Validators;

namespace TeraBank.Application.Accounts.Commands.RegisterAccount;

public sealed class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}
