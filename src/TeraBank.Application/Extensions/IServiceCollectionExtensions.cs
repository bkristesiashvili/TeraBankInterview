using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Commands.CreateBankAccount;
using TeraBank.Application.Accounts.Commands.RegisterAccount;
using TeraBank.Application.Transactions.Commands.MakeDeposit;
using TeraBank.Application.Transactions.Commands.TransferMoney;
using TeraBank.Application.Transactions.Commands.WithdrawalMoney;

namespace TeraBank.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddMediatR(configurations =>
        {
            configurations.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IPipelineBehavior<MakeDepositCommand, IResponse>, MakeDepositCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TransferMoneyCommand, IResponse>, TransferMoneyCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<RegisterAccountCommand, IResponse>, RegisterAccountCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<WithdrawalMoneyCommand, IResponse>, WithdrawalMoneyCommandvalidationBehavior>();
        services.AddScoped<IPipelineBehavior<CreateBankAccountCommand, IResponse>, CreateBankAccountCommandValidationBehavior>();
    }
}
