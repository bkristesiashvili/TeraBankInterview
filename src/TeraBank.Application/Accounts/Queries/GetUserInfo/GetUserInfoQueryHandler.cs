using Microsoft.AspNetCore.Identity;

using TeraBank.Application.Abstractions.Mediator.Queries;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Queries.GetUserInfo.Responses;
using TeraBank.Application.Responses;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Accounts.Queries.GetUserInfo;

internal sealed class GetUserInfoQueryHandler(UserManager<User> userManager) : IQueryHandler<GetUserInfoQuery>
{
    private readonly UserManager<User> userManager = userManager;

    public async Task<IResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if(await userManager.FindByIdAsync(request.UserId.ToString()) is not User user)
        {
            return ApiResponse.Failure(System.Net.HttpStatusCode.NotFound)
                .AddErrorMessage("User not found!");
        }

        UserInfoResponse responseObject = new(user.UserName,
            user.Accounts.Select(x => new UserAccountResponse(x.IBan, x.Balance))
            .ToList());

        return ApiResponse.Success(responseObject);
    }
}
