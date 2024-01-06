using System.Net;
using TeraBank.Application.Responses;

namespace TeraBank.Application.Abstractions.Responses;

public abstract class ApiResponseBase : IResponse
{
    public bool IsSuccess { get; protected set; }
    public HttpStatusCode StatusCode { get; protected set; }
    public ICollection<Message> Messages { get; } = new List<Message>();

    protected void AddMessage(Message message)
    {
        if (message == null)
        {
            return;
        }

        Messages.Add(message);
    }

    protected static bool EnsureSuccessCode(HttpStatusCode statusCode)
        => (int)statusCode >= 200 && (int)statusCode <= 299;

    public IResponse AddSuccessMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            AddMessage(new Message(true, message));
        }

        return this;
    }

    public IResponse AddSuccessMessages(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            AddSuccessMessage(message);
        }

        return this;
    }

    public IResponse AddErrorMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            AddMessage(new Message(false, message));
        }

        return this;
    }

    public IResponse AddErrorMessages(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            AddErrorMessage(message);
        }

        return this;
    }

    public IResponse ChangeStatusCode(HttpStatusCode statusCode)
    {
        if (!EnsureSuccessCode(statusCode))
        {
            StatusCode = statusCode;
            IsSuccess = false;
        }
        else
        {
            StatusCode = statusCode;
            IsSuccess = true;
        }

        return this;
    }
}

