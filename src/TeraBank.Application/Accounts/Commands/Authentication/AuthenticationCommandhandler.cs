using Microsoft.AspNetCore.Identity;

using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Abstractions.Services;
using TeraBank.Application.Accounts.Commands.Authentication.Responses;
using TeraBank.Application.Responses;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Accounts.Commands.Authentication;

internal sealed class AuthenticationCommandhandler(UserManager<User> userManager,
    IAuthTokenService tokenService) : ICommandHandler<AuthenticationCommand>
{
    private readonly UserManager<User> userManager = userManager;
    private readonly IAuthTokenService tokenService = tokenService;

    public async Task<IResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        if(await userManager.FindByEmailAsync(request.Email) is not User user)
        {
            return ApiResponse.Failure()
                .AddErrorMessage("Incorrect email!");
        }

        if(!await userManager.CheckPasswordAsync(user, request.Password))
        {
            return ApiResponse.Failure()
                .AddErrorMessage("Invalid password!");
        }

        AuthResponse response = new(
            await tokenService.BuildAuthToken(user),
            "Refresh token Placeholder",
            tokenService.ExpirationTime);

        return ApiResponse.Success(response, System.Net.HttpStatusCode.Accepted)
            .AddSuccessMessage("Authentication succeed");
    }
}