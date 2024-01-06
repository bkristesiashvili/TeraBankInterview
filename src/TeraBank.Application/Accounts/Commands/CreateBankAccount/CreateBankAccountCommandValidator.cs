using FluentValidation;

namespace TeraBank.Application.Accounts.Commands.CreateBankAccount;

public sealed class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
{
    public CreateBankAccountCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
