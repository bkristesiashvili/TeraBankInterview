using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;
using TeraBank.Domain.Abstractions.Database.Repositories.Queries;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Transactions.Commands.WithdrawalMoney;

internal sealed class WithdrawalMoneyCommandHandler(IUnitOfWork unitOfWork,
    ITransactionCommandRepository transactionCommandRepository,
    IBankAccountQueryRepository accountQueryRepository) : ICommandHandler<WithdrawalMoneyCommand>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly ITransactionCommandRepository transactionCommandRepository = transactionCommandRepository;
    private readonly IBankAccountQueryRepository accountQueryRepository = accountQueryRepository;

    public async Task<IResponse> Handle(WithdrawalMoneyCommand request, CancellationToken cancellationToken)
    {
        if (await accountQueryRepository.FindByIBan(request.IBan) is not BankAccount sender)
        {
            return ApiResponse.Failure(System.Net.HttpStatusCode.NotFound)
                .AddErrorMessage($"IBan '{request.IBan}' not found!");
        }

        Transaction depositTransaction = Transaction.Withdrawal(sender.Id, request.Amount);
        sender.TransferOrWithdrawBalance(depositTransaction);

        await transactionCommandRepository.CreateAsync(depositTransaction, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(System.Net.HttpStatusCode.Accepted)
            .AddSuccessMessage($"Amount: '{request.Amount}' withdrawn successfully.");
    }
}
