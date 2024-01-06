using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Api.ExceptionHandlers;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        IResponse response = ApiResponse
            .Failure()
            .AddErrorMessage(exception.Message);

        httpContext.Response.StatusCode = (int)response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}