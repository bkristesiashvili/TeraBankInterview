using FluentValidation;

using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

namespace TeraBank.Application.Accounts.Commands.Authentication;

internal sealed class AuthenticationCommandVaalidationBehavior(IValidator<AuthenticationCommand> validator) 
    : ValidationBehavior<AuthenticationCommand>(validator)
{ }
