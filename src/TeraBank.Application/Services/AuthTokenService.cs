using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using TeraBank.Application.Abstractions.Services;
using TeraBank.Application.Options;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Services;

internal sealed class AuthTokenService(SignInManager<User> signInManager,
    IOptions<AuthenticationOption> options) : IAuthTokenService
{
    private readonly SignInManager<User> signInManager = signInManager;
    private readonly IOptions<AuthenticationOption> options = options;

    public int ExpirationTime => options.Value.ExpirationSeconds;

    public Task<string> BuildAuthToken(User user, bool rememberMe = false)
    {
        ArgumentNullException.ThrowIfNull(user);

        SymmetricSecurityKey key = new(options.Value.EncruptionKeyBytes.ToArray());

        return BuildAccessToken(user, key, options.Value.Audience, rememberMe: rememberMe);
    }

    public Task<string> BuildRefreshToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateRefreshToken(User user, string refreshToken)
    {
        throw new NotImplementedException();
    }

    #region PRIVATE MEMBERS

    private async Task<string> BuildAccessToken(User user,
        SecurityKey key,
        string audience,
        string algorithms = SecurityAlgorithms.HmacSha256,
        bool rememberMe = false)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(key);
        ArgumentException.ThrowIfNullOrEmpty(algorithms);

        var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(user);

        List<Claim> claims = [.. claimsPrincipal.Claims];

        SigningCredentials signingCreds = new(key, algorithms);

        var token = new JwtSecurityToken(options.Value.Issuer,
            audience,
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddSeconds(options.Value.ExpirationSeconds),
            signingCredentials: signingCreds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion
}
