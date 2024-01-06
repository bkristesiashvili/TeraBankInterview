using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;
using TeraBank.Domain.Abstractions.Database.Repositories.Queries;
using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Entities;
using TeraBank.Application.Responses;
using TeraBank.Application.Abstractions.Responses;

namespace TeraBank.Application.Transactions.Commands.TransferMoney;

internal sealed class TransferMoneyCommandHandler(IUnitOfWork unitOfWork,
    ITransactionCommandRepository transactionCommandRepository,
    IBankAccountQueryRepository accountQueryRepository) : ICommandHandler<TransferMoneyCommand>
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly ITransactionCommandRepository transactionCommandRepository = transactionCommandRepository;
    private readonly IBankAccountQueryRepository accountQueryRepository = accountQueryRepository;

    public async Task<IResponse> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        if (await accountQueryRepository.FindByIBan(request.SenderIBan) is not BankAccount sender)
        {
            return ApiResponse.Failure(System.Net.HttpStatusCode.NotFound)
                .AddErrorMessage($"Sender '{request.SenderIBan}' not found!");
        }

        if (await accountQueryRepository.FindByIBan(request.ReceiverIBan) is not BankAccount receiver)
        {
            return ApiResponse.Failure(System.Net.HttpStatusCode.NotFound)
                .AddErrorMessage($"Sender '{request.ReceiverIBan}' not found!");
        }

        var (send,receive) = Transaction.Transfer(sender.Id, receiver.Id, request.Amount);

        sender.TransferOrWithdrawBalance(send);
        receiver.ReceiveAmountTransaction(receive);

        await transactionCommandRepository.CreateAsync(send, cancellationToken);
        await transactionCommandRepository.CreateAsync(receive, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(System.Net.HttpStatusCode.Accepted)
            .AddSuccessMessage($"Amount: '{request.Amount}' to IBAN: '{request.ReceiverIBan}' transfered successfully.");
    }
}
