using FluentValidation;

using Microsoft.AspNetCore.Identity;

using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Responses;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Accounts.Commands.Authentication;

public sealed record AuthenticationCommand(string Email, string Password): ICommand;

public sealed class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
{
    public AuthenticationCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}

internal sealed class AuthenticationCommandVaalidationBehavior(IValidator<AuthenticationCommand> validator) 
    : ValidationBehavior<AuthenticationCommand>(validator)
{ }


internal sealed class AuthenticationCommandhandler(UserManager<User> userManager) : ICommandHandler<AuthenticationCommand>
{
    private readonly UserManager<User> userManager = userManager;

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

        return ApiResponse.Success(System.Net.HttpStatusCode.Accepted)
            .AddSuccessMessage("Authentication succeed");
    }
}