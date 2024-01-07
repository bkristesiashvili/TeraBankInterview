using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using TeraBank.Api.ExceptionHandlers;
using TeraBank.Api.Extensions;
using TeraBank.Api.Options;
using TeraBank.Api.Results;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Commands.RegisterAccount;
using TeraBank.Application.Accounts.Queries.GetUserInfo;
using TeraBank.Application.Extensions;
using TeraBank.Application.Transactions.Commands.MakeDeposit;
using TeraBank.Application.Transactions.Commands.TransferMoney;
using TeraBank.Application.Transactions.Commands.WithdrawalMoney;
using TeraBank.Common.Extensions;
using TeraBank.Infrastructure.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddBankDbContext(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.Configure<AuthenticationOption>(builder.Configuration.GetSection(AuthenticationOption.OptionKey));

AuthenticationOption option = builder.Services.GetOption<AuthenticationOption>().Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = option.Issuer,
            ValidAudience = option.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(option.EncruptionKeyBytes.ToArray())
        };
    });

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseExceptionHandler();
await app.UseAutoMigration();

#region ACCOUNT ENDPOINTS

RouteGroupBuilder authGroup = app.MapGroup("auth");

authGroup.MapPost("register", async (ISender sender,
    [FromBody] RegisterAccountCommand request,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(request, cancellationToken);
    return StatusCodeResults.Response(result);

}).Produces<IResponse>();

authGroup.MapPost("login", async (ISender sender,
    CancellationToken cancellationToken) =>
{
    return StatusCodeResults.Response( System.Net.HttpStatusCode.OK, new {Message = "Developing process"});

}).Produces<IResponse>();

#endregion

#region USERS INFO ENDPOINTS

RouteGroupBuilder usersGroup = app.MapGroup("users").RequireAuthorization();

usersGroup.MapGet("info/{userId:guid}", async (ISender sender,
    Guid userId,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(new GetUserInfoQuery(userId), cancellationToken);
    return StatusCodeResults.Response(result);
});

#endregion

#region TRANSACTION ENDPOINTS

var transactionGroup = app.MapGroup("transactions").RequireAuthorization();

transactionGroup.MapPost("deposit", async (ISender sender,
    MakeDepositCommand request,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(request, cancellationToken);
    return StatusCodeResults.Response(result);
});

transactionGroup.MapPost("withdraw", async (ISender sender,
    WithdrawalMoneyCommand request,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(request, cancellationToken);
    return StatusCodeResults.Response(result);
});

transactionGroup.MapPost("transfer", async (ISender sender,
    TransferMoneyCommand request,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(request, cancellationToken);
    return StatusCodeResults.Response(result);
});

#endregion

app.Run();