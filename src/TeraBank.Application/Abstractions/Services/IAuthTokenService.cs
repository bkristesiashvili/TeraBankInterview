using TeraBank.Domain.Entities;

namespace TeraBank.Application.Abstractions.Services;

internal interface IAuthTokenService
{
    Task<string> BuildAuthToken(User user, bool rememberMe = false);

    Task<string> BuildRefreshToken(User user);

    Task<bool> ValidateRefreshToken(User user, string refreshToken);

    public int ExpirationTime { get; }
}
