using TeraBank.Domain.Abstractions.Database.Entities;
using TeraBank.Domain.Exceptions;

namespace TeraBank.Domain.Entities;

public class BankAccount(Guid ownerId) : BaseEntity
{
    public decimal Balance { get; set; }
    public required string IBan { get; set; }

    public Guid OwnerId { get; protected set; } = ownerId;
    public virtual User Owner { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

    public IReadOnlyCollection<Transaction> TxIn()
        => Transactions.Where(x => x.Amount > decimal.Zero).ToList();

    public IReadOnlyCollection<Transaction> TxOut()
        => Transactions.Where(x => x.Amount < decimal.Zero).ToList();

    public void DepositBalance(Transaction transaction)
    {
        if (ValidateDepositTransaction(transaction))
        {
            Balance += transaction.Amount;
        }
    }

    public void TransferOrWithdrawBalance(Transaction transaction)
    {
        if (ValidateTransferOrWithdrawTransaction(transaction))
        {
            Balance += transaction.Amount;
        }

    }

    public void ReceiveAmountTransaction(Transaction transaction)
    {
        if (ValidateReceiveTransaction(transaction))
        {
            Balance += transaction.Amount;
        }
    }

    #region PRIVATE METHODS

    private static bool ValidateDepositTransaction(Transaction transaction)
    {
        return transaction is not null && transaction.Type == Enums.TransactionTypes.Deposit;
    }

    private bool ValidateTransferOrWithdrawTransaction(Transaction transaction)
    {
        if (Balance < Math.Abs(transaction.Amount))
        {
            throw new NotEnoughBalanceException();
        }

        return transaction is not null &&
            transaction.Type != Enums.TransactionTypes.Deposit &&
            transaction.Type != Enums.TransactionTypes.Receive;
    }

    private static bool ValidateReceiveTransaction(Transaction transaction)
    {
        return transaction is not null && transaction.Type == Enums.TransactionTypes.Receive;
    }

    #endregion
}
