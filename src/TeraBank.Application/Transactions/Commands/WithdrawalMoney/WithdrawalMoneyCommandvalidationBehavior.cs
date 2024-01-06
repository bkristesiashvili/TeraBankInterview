using FluentValidation;

using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

namespace TeraBank.Application.Transactions.Commands.WithdrawalMoney;

internal sealed class WithdrawalMoneyCommandvalidationBehavior(IValidator<WithdrawalMoneyCommand> validator)
    : ValidationBehavior<WithdrawalMoneyCommand>(validator)
{ }
