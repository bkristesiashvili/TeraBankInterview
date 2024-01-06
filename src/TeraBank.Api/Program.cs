using MediatR;

using Microsoft.AspNetCore.Mvc;

using TeraBank.Api.ExceptionHandlers;
using TeraBank.Api.Extensions;
using TeraBank.Api.Results;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Commands.RegisterAccount;
using TeraBank.Application.Accounts.Queries.GetUserInfo;
using TeraBank.Application.Extensions;
using TeraBank.Application.Transactions.Commands.MakeDeposit;
using TeraBank.Application.Transactions.Commands.TransferMoney;
using TeraBank.Application.Transactions.Commands.WithdrawalMoney;
using TeraBank.Infrastructure.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddBankDbContext(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
await app.UseAutoMigration();

#region ACCOUNT ENDPOINTS

var accountGroup = app.MapGroup("accounts");

accountGroup.MapPost("register", async (ISender sender,
    [FromBody] RegisterAccountCommand request,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(request, cancellationToken);
    return StatusCodeResults.Response(result);

}).Produces<IResponse>();

accountGroup.MapGet("info/{userId:guid}", async (ISender sender,
    Guid userId,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(new GetUserInfoQuery(userId), cancellationToken);
    return StatusCodeResults.Response(result);
});

#endregion

#region TRANSACTION ENDPOINTS

var transactionGroup = app.MapGroup("transactions");

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