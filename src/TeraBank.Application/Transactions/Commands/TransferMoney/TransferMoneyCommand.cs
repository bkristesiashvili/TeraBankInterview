using TeraBank.Application.Abstractions.Mediator.Commands;

namespace TeraBank.Application.Transactions.Commands.TransferMoney;

public sealed record TransferMoneyCommand(string SenderIBan,
    string ReceiverIBan,
    decimal Amount) : ICommand;
