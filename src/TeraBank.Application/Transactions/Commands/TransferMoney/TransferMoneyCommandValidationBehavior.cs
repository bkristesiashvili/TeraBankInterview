using FluentValidation;

using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

namespace TeraBank.Application.Transactions.Commands.TransferMoney;

internal sealed class TransferMoneyCommandValidationBehavior(IValidator<TransferMoneyCommand> validator)
    : ValidationBehavior<TransferMoneyCommand>(validator)
{ }
