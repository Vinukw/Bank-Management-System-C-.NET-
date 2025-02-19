using System;

public abstract class Transaction
{
    protected decimal _amount;
    protected bool _executed;
    protected bool _success;
    protected bool _reversed;
    protected DateTime _dateStamp;

    public decimal Amount => _amount;
    public bool Executed => _executed;
    public bool Success => _success;
    public bool Reversed => _reversed;
    public DateTime DateStamp => _dateStamp;

    public Transaction(decimal amount)
    {
        _amount = amount;
        _executed = false;
        _success = false;
        _reversed = false;
        _dateStamp = DateTime.Now;
    }

    public abstract void Execute();
    public abstract void Rollback();
    public virtual void Print()
    {
        Console.WriteLine($"Transaction Details - Amount: {_amount}, Executed: {_executed}, Success: {_success}, Reversed: {_reversed}, DateStamp: {_dateStamp}");
    }
}
