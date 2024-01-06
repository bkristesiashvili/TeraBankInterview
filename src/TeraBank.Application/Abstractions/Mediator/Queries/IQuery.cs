using MediatR;
using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Application.Abstractions.Mediator.Queries;

public interface IQuery : IRequest<IResponse> { }
