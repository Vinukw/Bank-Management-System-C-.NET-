using System;

public class DepositTransaction : Transaction
{
    private Account _account;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        if (_executed)
            throw new InvalidOperationException("Transaction has already been executed.");

        _dateStamp = DateTime.Now;
        _executed = true;
        _success = _account.Deposit(_amount);
    }

    public override void Rollback()
    {
        if (!_executed)
            throw new InvalidOperationException("Transaction has not been executed yet.");

        _dateStamp = DateTime.Now;

        if (_reversed)
            throw new InvalidOperationException("Transaction has already been reversed.");

        if (_success)
        {
            _account.Withdraw(_amount);
            _reversed = true;
        }
        else
        {
            throw new InvalidOperationException("Transaction was not successful, cannot rollback.");
        }
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Deposit Transaction Details - Account: {_account.Name}, Executed: {_executed}, Success: {_success}, Reversed: {_reversed}");
    }
}
