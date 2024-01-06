using TeraBank.Application.Abstractions.Mediator.Commands;

namespace TeraBank.Application.Transactions.Commands.WithdrawalMoney;

public sealed record WithdrawalMoneyCommand(string IBan, decimal Amount) : ICommand;
