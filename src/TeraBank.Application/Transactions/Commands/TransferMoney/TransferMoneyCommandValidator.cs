using FluentValidation;

namespace TeraBank.Application.Transactions.Commands.TransferMoney;

public sealed class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
{
    public TransferMoneyCommandValidator()
    {
        RuleFor(x => x.SenderIBan).NotEmpty();
        RuleFor(x => x.ReceiverIBan).NotEmpty();
        RuleFor(x => x.Amount)
            .GreaterThan(decimal.Zero)
            .LessThanOrEqualTo(1000_000_000);
    }
}
