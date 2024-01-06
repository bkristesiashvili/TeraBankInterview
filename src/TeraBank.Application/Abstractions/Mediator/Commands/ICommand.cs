using MediatR;

using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Application.Abstractions.Mediator.Commands;

public interface ICommand : IRequest<IResponse> { }
