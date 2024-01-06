using TeraBank.Application.Abstractions.Mediator.Commands;

namespace TeraBank.Application.Accounts.Commands.RegisterAccount;

public sealed record RegisterAccountCommand(string Email, string Password) : ICommand;
