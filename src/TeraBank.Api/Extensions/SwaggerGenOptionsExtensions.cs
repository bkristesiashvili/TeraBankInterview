using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace TeraBank.Api.Extensions;

internal static class SwaggerGenOptionsExtensions
{
    internal static void AddSecurityDefinition(this SwaggerGenOptions swaggerGenOptions)
    {
        ArgumentNullException.ThrowIfNull(swaggerGenOptions);

        OpenApiSecurityScheme securitySchema = new()
        {
            Description = @"JWT Authorization header the Bearer Scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        };

        swaggerGenOptions.AddSecurityDefinition("Bearer", securitySchema);
    }

    internal static void AddSecurityRequirement(this SwaggerGenOptions swaggerGenOptions)
    {
        ArgumentNullException.ThrowIfNull(swaggerGenOptions);

        OpenApiSecurityScheme securityRequirementSchema = new()
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        };

        OpenApiSecurityRequirement securityRequiremens = new()
            {
                { securityRequirementSchema, new List<string>() }
            };

        swaggerGenOptions.AddSecurityRequirement(securityRequiremens);
    }
}
