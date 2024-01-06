using MediatR;

using Microsoft.AspNetCore.Identity;

using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Commands.CreateBankAccount;
using TeraBank.Application.Responses;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Accounts.Commands.RegisterAccount;

internal sealed class RegisterAccountCommandHandler(UserManager<User> userManager,
    ISender sender) : ICommandHandler<RegisterAccountCommand>
{
    private readonly UserManager<User> userManager = userManager;
    private readonly ISender sender = sender;

    public async Task<IResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        User newUser = new()
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
        IdentityResult registrationResult = await userManager.CreateAsync(newUser, request.Password);

        if (!registrationResult.Succeeded)
        {
            return ApiResponse.Failure()
                .AddErrorMessages(registrationResult.Errors.Select(x => x.Description));
        }

        var createBankAccountResult = await sender.Send(new CreateBankAccountCommand(newUser.Id), cancellationToken);
        if (!createBankAccountResult.IsSuccess)
        {
            return createBankAccountResult;
        }

        return ApiResponse.Success(System.Net.HttpStatusCode.Created)
            .AddSuccessMessage($"Account '{request.Email}' registered successfully.")
            .AddSuccessMessages(createBankAccountResult.Messages.Select(x => x.Text));
    }
}
