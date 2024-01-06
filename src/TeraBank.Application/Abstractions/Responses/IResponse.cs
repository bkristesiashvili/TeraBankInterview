using System.Net;
using TeraBank.Application.Responses;

namespace TeraBank.Application.Abstractions.Responses;

public interface IResponse
{
    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; }
    public ICollection<Message> Messages { get; }

    IResponse AddSuccessMessage(string message);
    IResponse AddSuccessMessages(IEnumerable<string> messages);
    IResponse AddErrorMessage(string messate);
    IResponse AddErrorMessages(IEnumerable<string> messages);

    IResponse ChangeStatusCode(HttpStatusCode statusCode);
}