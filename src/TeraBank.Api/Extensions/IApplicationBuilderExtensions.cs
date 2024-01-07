using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TeraBank.Api.Results;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Commands.RegisterAccount;
using TeraBank.Application.Accounts.Queries.GetUserInfo;
using TeraBank.Application.Transactions.Commands.MakeDeposit;
using TeraBank.Application.Transactions.Commands.TransferMoney;
using TeraBank.Application.Transactions.Commands.WithdrawalMoney;
using TeraBank.Domain.Abstractions.Database;

namespace TeraBank.Api.Extensions;

internal static class IApplicationBuilderExtensions
{
    internal static async ValueTask UseAutoMigration(this IApplicationBuilder applicationBuilder)
    {
        ArgumentNullException.ThrowIfNull(applicationBuilder);

        using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        IBankDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<IBankDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    internal static void UseAuthEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ArgumentNullException.ThrowIfNull(routeBuilder);

        RouteGroupBuilder authGroup = routeBuilder.MapGroup("auth");

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
            return StatusCodeResults.Response(System.Net.HttpStatusCode.OK, new { Message = "Developing process" });

        }).Produces<IResponse>();
    }

    internal static void UseTransactionEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ArgumentNullException.ThrowIfNull(routeBuilder);

        var transactionGroup = routeBuilder.MapGroup("transactions").RequireAuthorization();

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
    }

    internal static void UseUserEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ArgumentNullException.ThrowIfNull(routeBuilder);

        RouteGroupBuilder usersGroup = routeBuilder.MapGroup("users").RequireAuthorization();

        usersGroup.MapGet("info/{userId:guid}", async (ISender sender,
            Guid userId,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetUserInfoQuery(userId), cancellationToken);
            return StatusCodeResults.Response(result);
        });
    }
}
