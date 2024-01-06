using TeraBank.Application.Abstractions.Mediator.Queries;

namespace TeraBank.Application.Accounts.Queries.GetUserInfo;

public sealed record GetUserInfoQuery(Guid UserId) : IQuery;
