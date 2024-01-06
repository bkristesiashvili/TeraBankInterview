using FluentValidation;

namespace TeraBank.Application.Transactions.Commands.MakeDeposit;

public sealed class MakeDepositCommandValidator : AbstractValidator<MakeDepositCommand>
{
    public MakeDepositCommandValidator()
    {
        RuleFor(x => x.IBan).NotEmpty().Length(22, 25);
        RuleFor(x => x.Amount)
            .GreaterThan(decimal.Zero)
            .LessThanOrEqualTo(1_000_000_000);
    }
}
