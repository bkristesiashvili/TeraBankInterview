using TeraBank.Application.Abstractions.Mediator.Commands;

namespace TeraBank.Application.Accounts.Commands.CreateBankAccount;

public sealed record CreateBankAccountCommand(Guid UserId) : ICommand;
