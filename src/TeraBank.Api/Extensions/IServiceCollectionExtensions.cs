using Microsoft.OpenApi.Models;

namespace TeraBank.Api.Extensions;

internal static class IServiceCollectionExtensions
{
    internal static void ConfigureSwagger(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSwaggerGen(setup =>
        {
            setup.AddSecurityDefinition();

            setup.AddSecurityRequirement();
        });
    }
}
