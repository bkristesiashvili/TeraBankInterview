namespace TeraBank.Application.Abstractions.Responses;

public sealed record Message(bool IsSuccess, string Text);
