using System;
using System.Collections.Generic;

public class Bank
{
    private List<Account> _accounts = new List<Account>();
    private List<Transaction> _transactions = new List<Transaction>();

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account? GetAccount(string name)
    {
        foreach (var account in _accounts)
        {
            if (account.Name == name)
            {
                return account;
            }
        }
        return null; // Explicitly returning null to indicate no match found
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        transaction.Execute();
    }

    public void RollbackTransaction(Transaction transaction)
    {
        transaction.Rollback();
    }

    public List<Account> GetAccounts()
    {
        return _accounts;
    }

    public void PrintTransactionHistory()
    {
        Console.WriteLine("\nTransaction History:");
        for (int i = 0; i < _transactions.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _transactions[i].Print();
        }
    }
        public Transaction GetTransactionByIndex(int index)
    {
        return _transactions[index]; // Retrieve the transaction by index
    }

    public int GetTransactionsCount()
    {
        return _transactions.Count; // Return the count of all transactions
    }

}
