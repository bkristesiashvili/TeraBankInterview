using FluentValidation;

namespace TeraBank.Application.Accounts.Commands.Authentication;

public sealed class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
{
    public AuthenticationCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}
