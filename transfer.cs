using System;

public class TransferTransaction : Transaction
{
    private readonly Account _fromAccount;
    private readonly Account _toAccount;
    private readonly DepositTransaction _deposit;
    private readonly WithdrawTransaction _withdraw;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _deposit = new DepositTransaction(_toAccount, amount);
        _withdraw = new WithdrawTransaction(_fromAccount, amount);
    }

    public override void Execute()
    {
        if (_executed)
            throw new InvalidOperationException("Transfer already executed.");

        _dateStamp = DateTime.Now;

        try
        {
            _withdraw.Execute();
            _deposit.Execute();
            _success = true;
            _executed = true;
        }
        catch (InvalidOperationException ex)
        {
            _success = false;
            throw new InvalidOperationException($"Transfer failed: {ex.Message}");
        }
    }

    public override void Rollback()
    {
        if (!_executed)
            throw new InvalidOperationException("Transfer not executed.");

        _dateStamp = DateTime.Now;

        try
        {
            _deposit.Rollback();
            _withdraw.Rollback();
            _reversed = true;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Rollback failed: {ex.Message}");
        }
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Transferred ${_amount} from {_fromAccount.Name}'s account to {_toAccount.Name}'s account.");
        _deposit.Print();
        _withdraw.Print();
    }
}
