namespace Remita.Models.Exceptions;

public class AccountLockedException : Exception
{
    public AccountLockedException(string? message) : base(message)
    {

    }
}
