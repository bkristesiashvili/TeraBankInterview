using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Responses;
using TeraBank.Domain.Exceptions;

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
            .Failure();
#if DEBUG
            response.AddErrorMessage(exception.Message);
#else
        string errorMessage = GetKnownExceptionMessage(exception);
        response.AddErrorMessage(errorMessage);
#endif
        httpContext.Response.StatusCode = (int)response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    #region PRIVATE METHODS

    private static string GetKnownExceptionMessage(Exception exception)
    {
        if(exception is null)
        {
            return string.Empty;
        }

        bool isKnownException = exception is NotEnoughBalanceException ||
             exception is InvalidOperationException;

        return isKnownException
            ? exception.Message
            : new("Something went wrong on the server!");
    }

    #endregion
}