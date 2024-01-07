using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using TeraBank.Application.Options;
using TeraBank.Common.Extensions;

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

    internal static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<AuthenticationOption>(configuration.GetSection(AuthenticationOption.OptionKey));

        AuthenticationOption option = services.GetOption<AuthenticationOption>().Value;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build());
    }
}
