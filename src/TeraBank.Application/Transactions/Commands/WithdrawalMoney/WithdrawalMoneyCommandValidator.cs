using FluentValidation;

namespace TeraBank.Application.Transactions.Commands.WithdrawalMoney;

public sealed class WithdrawalMoneyCommandValidator : AbstractValidator<WithdrawalMoneyCommand>
{
    public WithdrawalMoneyCommandValidator()
    {
        RuleFor(x => x.IBan).NotEmpty();
        RuleFor(x => x.Amount)
            .GreaterThan(decimal.Zero)
            .LessThanOrEqualTo(100_000);
    }
}
