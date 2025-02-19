using System;

public class Account
{
    private decimal _balance;
    public string Name { get; }
    
    public decimal Balance
    {
        get { return _balance; }
    }

    public Account(string name, decimal balance)
    {
        Name = name;
        _balance = balance;
    }

    public bool Deposit(decimal amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            return true;
        }
        return false;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= _balance)
        {
            _balance -= amount;
            return true;
        }
        return false;
    }

    public void Print()
    {
        Console.WriteLine("Account Name: " + Name + ", Balance: " + _balance);
    }
}
