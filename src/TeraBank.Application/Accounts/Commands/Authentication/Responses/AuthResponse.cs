namespace TeraBank.Application.Accounts.Commands.Authentication.Responses;

internal sealed record AuthResponse(string AccessToken,
    string RefreshToken,
    int ExpiresIn);