using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Responses;
using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;
using TeraBank.Domain.Abstractions.Database.Repositories.Queries;
using TeraBank.Domain.Entities;

namespace TeraBank.Application.Transactions.Commands.MakeDeposit;

internal sealed class MakeDepositCommandHandler(IUnitOfWork unitOfWork,
    IBankAccountQueryRepository accountQueryRepository,
    ITransactionCommandRepository transactionCommandRepository) : ICommandHandler<MakeDepositCommand>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly ITransactionCommandRepository transactionCommandRepository = transactionCommandRepository;
    private readonly IBankAccountQueryRepository accountQueryRepository = accountQueryRepository;

    public async Task<IResponse> Handle(MakeDepositCommand request, CancellationToken cancellationToken)
    {
        if (await accountQueryRepository.FindByIBan(request.IBan) is not BankAccount receiver)
        {
            return ApiResponse.Failure(System.Net.HttpStatusCode.NotFound)
                .AddErrorMessage($"IBan '{request.IBan}' not found!");
        }

        Transaction depositTransaction = Transaction.Deposit(receiver.Id, request.Amount);
        receiver.DepositBalance(depositTransaction);

        await transactionCommandRepository.CreateAsync(depositTransaction, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(System.Net.HttpStatusCode.Accepted)
            .AddSuccessMessage($"Amount: '{request.Amount}' deposited successfully.");
    }
}
