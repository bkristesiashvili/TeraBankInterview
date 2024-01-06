using MediatR;

using TeraBank.Application.Abstractions.Mediator.Commands;
using TeraBank.Application.Abstractions.Responses;
using TeraBank.Application.Accounts.Queries.GetUserInfo;
using TeraBank.Application.Responses;
using TeraBank.Domain.Abstractions.Database;
using TeraBank.Domain.Abstractions.Database.Repositories.Commands;

namespace TeraBank.Application.Accounts.Commands.CreateBankAccount;

internal sealed class CreateBankAccountCommandHandler(ISender sender,
    IUnitOfWork unitOfWork,
    IBankAccountCommandRepository accountCommandRepository) : ICommandHandler<CreateBankAccountCommand>
{
    private readonly IBankAccountCommandRepository accountCommandRepository = accountCommandRepository;
    private readonly ISender sender = sender;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<IResponse> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var checkUserExistence = await sender.Send(new GetUserInfoQuery(request.UserId), cancellationToken);
        if (!checkUserExistence.IsSuccess)
        {
            return checkUserExistence;
        }

        var newBankAccount = new Domain.Entities.BankAccount(request.UserId)
        {
            IBan = $"GE00TB{DateTime.Now.Ticks}"
        };
        await accountCommandRepository.CreateAsync(newBankAccount,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(System.Net.HttpStatusCode.Created)
            .AddSuccessMessage($"Bank account '{newBankAccount.IBan}' created successfully.");
    }
}