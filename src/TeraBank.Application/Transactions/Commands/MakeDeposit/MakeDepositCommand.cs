using TeraBank.Application.Abstractions.Mediator.Commands;

namespace TeraBank.Application.Transactions.Commands.MakeDeposit;

public sealed record MakeDepositCommand(string IBan,
    decimal Amount) : ICommand;
