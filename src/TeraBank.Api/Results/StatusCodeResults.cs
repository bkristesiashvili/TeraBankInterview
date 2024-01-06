using System.Net;
using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Api.Results;

internal sealed class StatusCodeResults : IResult
{
    private readonly HttpStatusCode _statusCode;
    private readonly object _responseBody;

    private StatusCodeResults(HttpStatusCode httpStatus, object response)
    {
        _statusCode = httpStatus;
        _responseBody = response;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = (int)_statusCode;
        await httpContext.Response.WriteAsJsonAsync(_responseBody);
    }

    public static IResult Response(HttpStatusCode statusCode, object response = default)
        => new StatusCodeResults(statusCode, response);

    public static IResult Response(IResponse response)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        return Response(response.StatusCode, response);
    }
}
