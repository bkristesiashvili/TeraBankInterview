using FluentValidation;

using MediatR;

using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Application.Abstractions.Mediator.Commands.Behaviors;

internal abstract class ValidationBehavior<TCommand> : IPipelineBehavior<TCommand, IResponse>
    where TCommand : class, ICommand
{
    private readonly IValidator<TCommand> _validator;
    protected readonly IResponse ErrorResponse;

    protected ValidationBehavior(IValidator<TCommand> validator)
    {
        _validator = validator;
        ErrorResponse = ApiResponse.Failure();
    }

    public async Task<IResponse> Handle(TCommand request, RequestHandlerDelegate<IResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return ErrorResponse.AddErrorMessages(errors);
        }

        return await next();
    }
}
