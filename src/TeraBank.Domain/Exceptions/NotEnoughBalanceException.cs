namespace TeraBank.Domain.Exceptions;

public sealed class NotEnoughBalanceException : Exception
{
    public NotEnoughBalanceException() : base("Not enough money on balance!")
    { }
}
