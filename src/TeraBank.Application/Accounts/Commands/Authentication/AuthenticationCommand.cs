using TeraBank.Application.Abstractions.Mediator.Commands;

namespace TeraBank.Application.Accounts.Commands.Authentication;

public sealed record AuthenticationCommand(string Email, string Password): ICommand;
