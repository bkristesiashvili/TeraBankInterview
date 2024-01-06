using FluentValidation;

using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

namespace TeraBank.Application.Accounts.Commands.CreateBankAccount;

internal sealed class CreateBankAccountCommandValidationBehavior(IValidator<CreateBankAccountCommand> validator) 
    : ValidationBehavior<CreateBankAccountCommand>(validator)
{ }
