using MediatR;
using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Application.Abstractions.Mediator.Commands;

internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IResponse>
    where TCommand : class, ICommand
{ }