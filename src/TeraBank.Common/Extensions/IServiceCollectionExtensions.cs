using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TeraBank.Common.Extensions;

public static class IServiceCollectionExtensions
{
    public static IOptions<T> GetOption<T>(this IServiceCollection services)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(services);

        return services.BuildServiceProvider().GetRequiredService<IOptions<T>>();
    }
}
