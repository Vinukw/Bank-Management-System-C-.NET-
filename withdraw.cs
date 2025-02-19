using System;

public class WithdrawTransaction : Transaction
{
    private Account _account;

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        if (_executed)
            throw new InvalidOperationException("Withdraw operation already executed.");

        _dateStamp = DateTime.Now;

        if (_account.Withdraw(_amount))
        {
            _success = true;
            _executed = true;
        }
        else
        {
            _success = false;
            _executed = true;
            throw new InvalidOperationException("Insufficient funds or invalid amount.");
        }
    }

    public override void Rollback()
    {
        if (!_executed)
            throw new InvalidOperationException("Withdraw operation not executed.");

        _dateStamp = DateTime.Now;

        if (_success && !_reversed)
        {
            _account.Deposit(_amount);
            _reversed = true;
        }
        else
        {
            throw new InvalidOperationException("Withdraw operation not successful or already reversed.");
        }
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Withdraw Transaction Details - Account: {_account.Name}, Executed: {_executed}, Success: {_success}, Reversed: {_reversed}");
    }
}
