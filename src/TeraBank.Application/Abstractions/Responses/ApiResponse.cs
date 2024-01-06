using System.Net;

namespace TeraBank.Application.Abstractions.Responses;

public sealed class ApiResponse : ApiResponseBase, IResponse
{
    private ApiResponse(HttpStatusCode statusCode)
    {
        IsSuccess = EnsureSuccessCode(statusCode);
        StatusCode = statusCode;
    }

    public static IResponse Success(HttpStatusCode statusCode = HttpStatusCode.OK) => new ApiResponse(statusCode);

    public static IResponse Success<T>(T data, HttpStatusCode statusCode = HttpStatusCode.OK) => new ApiResponse<T>(data, statusCode);

    public static IResponse Failure(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        if (EnsureSuccessCode(statusCode))
        {
            throw new Exception("Invalid failure status code!");
        }

        return new ApiResponse(statusCode);
    }
}

public sealed class ApiResponse<T> : ApiResponseBase, IResponse
{
    public T Data { get; set; }

    internal ApiResponse(T data, HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
        IsSuccess = EnsureSuccessCode(statusCode);
        Data = IsSuccess ? data : default;
    }
}