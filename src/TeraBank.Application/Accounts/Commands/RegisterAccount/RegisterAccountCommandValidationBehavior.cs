using FluentValidation;

using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

namespace TeraBank.Application.Accounts.Commands.RegisterAccount;

internal sealed class RegisterAccountCommandValidationBehavior(IValidator<RegisterAccountCommand> validator) 
    : ValidationBehavior<RegisterAccountCommand>(validator)
{ }
