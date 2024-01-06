using MediatR;

using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Application.Abstractions.Mediator.Queries;

internal interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, IResponse>
    where TQuery : class, IQuery
{ }
