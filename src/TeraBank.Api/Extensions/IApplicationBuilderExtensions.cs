using Microsoft.EntityFrameworkCore;

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
}
