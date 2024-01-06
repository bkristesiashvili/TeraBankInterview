using TeraBank.Domain.Abstractions.Database.Entities;
using TeraBank.Domain.Entities.Enums;

namespace TeraBank.Domain.Entities;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    public TransactionStatuses Status { get; set; }
    public TransactionTypes Type { get; set; }

    public Guid AccountId { get; set; }
    public virtual BankAccount Account { get; set; }

    public Guid? SenderTransactionId { get; set; }
    public virtual Transaction SenderTransaction { get; set; }

    public static Transaction Deposit(Guid receiver, decimal amount)
    {
        ValidateAmount(amount);

        return new()
        {
            Amount = amount,
            AccountId = receiver,
            Type = TransactionTypes.Deposit,
            Status = TransactionStatuses.Confirmed
        };
    }

    public static (Transaction Send, Transaction Receive) Transfer(Guid senderId, Guid receiverId, decimal amount)
    {
        ValidateAmount(amount);

        Transaction send = new()
        {
            Amount = -1 * amount,
            AccountId = senderId,
            Type = TransactionTypes.Transfer,
            Status = TransactionStatuses.Confirmed
        };

        Transaction receive = new()
        {
            Amount = amount,
            AccountId = receiverId,
            Type = TransactionTypes.Receive,
            Status = TransactionStatuses.Confirmed,
            SenderTransactionId = send.Id
        };

        return (send, receive);
    }

    public static Transaction Withdrawal(Guid senderId, decimal amount)
    {
        ValidateAmount(amount);

        return new()
        {
            Amount = -1 * amount,
            AccountId = senderId,
            Type = TransactionTypes.Withdrawal,
            Status = TransactionStatuses.Confirmed
        };
    }

    #region PRIVATE METHODS

    private static void ValidateAmount(decimal amount)
    {
        if (amount <= decimal.Zero)
        {
            throw new InvalidOperationException("Invalid amount!");
        }
    }

    #endregion
}
