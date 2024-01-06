using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;
using TeraBank.Domain.Abstractions.Database.Repositories.Queries;
using TeraBank.Domain.Entities;
using TeraBank.Infrastructure.Database.Repositories.Commands;
using TeraBank.Infrastructure.Database.Repositories.Queries;

namespace TeraBank.Infrastructure.Database.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddBankDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddDbContext<BankDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("demo"), config =>
            {
                config.CommandTimeout(30);
                config.MigrationsAssembly(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                config.MigrationsHistoryTable("Migrations");
            });
            options.UseLazyLoadingProxies();
        });

        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<BankDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IBankDbContext>(provider => provider.GetRequiredService<BankDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBankAccountCommandRepository, BankAccountCommandRepository>();
        services.AddScoped<IBankAccountQueryRepository, BankAccountQueryRepository>();
        services.AddScoped<ITransactionCommandRepository, TransactionCommandRepository>();
    }
}
