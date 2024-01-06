using FluentValidation;
using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

namespace TeraBank.Application.Transactions.Commands.MakeDeposit;

internal sealed class MakeDepositCommandValidationBehavior(IValidator<MakeDepositCommand> validator)
    : ValidationBehavior<MakeDepositCommand>(validator)
{ }
