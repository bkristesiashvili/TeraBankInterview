namespace TeraBank.Application.Accounts.Queries.GetUserInfo.Responses;

internal sealed record UserAccountResponse(string IBan, decimal Balance);

internal sealed record UserInfoResponse(string UserName, 
    IReadOnlyCollection<UserAccountResponse> UserAccounts);