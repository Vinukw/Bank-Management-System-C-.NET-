using System;

public enum MenuOption
{
    Withdraw = 1,
    Deposit,
    Transfer,
    PrintAccounts,
    PrintTransactionHistory,
    Rollback,
    AddAccount,
    Quit
}

public class BankSystem
{
    private static Bank _bank = new Bank();

    public static void Main()
    {
        // Create some sample accounts
        Account account1 = new Account("Andrew Dalwood", 2000);
        Account account2 = new Account("Michael Klein", 3000);

        _bank.AddAccount(account1);
        _bank.AddAccount(account2);

        MenuOption option;
        do
        {
            // Display menu options
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Print Accounts");
            Console.WriteLine("5. Print Transaction History");
            Console.WriteLine("6. Rollback Transaction");
            Console.WriteLine("7. Add Account");
            Console.WriteLine("8. Quit");

            // Read user option
            option = ReadUserOption();

            // Respond to user option
            switch (option)
            {
                case MenuOption.Withdraw:
                    DoWithdraw(_bank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(_bank);
                    break;
                case MenuOption.Transfer:
                    DoTransfer(_bank);
                    break;
                case MenuOption.PrintAccounts:
                    DoPrintAccounts(_bank);
                    break;
                case MenuOption.PrintTransactionHistory:
                    DoPrintTransactionHistory(_bank);
                    break;
                case MenuOption.Rollback:
                    DoRollback(_bank);
                    break;
                case MenuOption.AddAccount:
                    DoAddAccount(_bank);
                    break;
                case MenuOption.Quit:
                    Console.WriteLine("Exiting program.");
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }
        } while (option != MenuOption.Quit);
    }

    public static MenuOption ReadUserOption()
    {
        int choice;
        bool validChoice;
        do
        {
            Console.Write("\nEnter your choice: ");
            string? userInput = Console.ReadLine();
            validChoice = int.TryParse(userInput, out choice);
            if (!validChoice || choice < 1 || choice > 8)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 8.");
                validChoice = false; // Reset validChoice flag to continue the loop
            }
        } while (!validChoice);

        return (MenuOption)choice;
    }

    public static void DoWithdraw(Bank bank)
    {
        Console.Write("Enter account name for withdrawal: ");
        string? name = Console.ReadLine(); // Allow null name input

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid account name.");
            return;
        }

        Account? account = bank.GetAccount(name);
        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }

        decimal amount;
        bool validAmount;
        do
        {
            Console.Write("Enter the amount to withdraw: ");
            string? userInput = Console.ReadLine();
            validAmount = decimal.TryParse(userInput, out amount) && amount > 0;

            if (!validAmount)
                Console.WriteLine("Invalid amount. Please enter a valid positive number.");
        } while (!validAmount);

        WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount);
        bank.ExecuteTransaction(withdrawTransaction);
        Console.WriteLine($"Withdrawal of ${amount} successful. Remaining balance: {account.Balance}");
    }

    public static void DoDeposit(Bank bank)
    {
        Console.Write("Enter account name for deposit: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid account name.");
            return;
        }

        Account? account = bank.GetAccount(name);
        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }

        decimal amount;
        bool validAmount;
        do
        {
            Console.Write("Enter the amount to deposit: ");
            string? userInput = Console.ReadLine();
            validAmount = decimal.TryParse(userInput, out amount) && amount > 0;

            if (!validAmount)
                Console.WriteLine("Invalid amount. Please enter a valid positive number.");
        } while (!validAmount);

        DepositTransaction depositTransaction = new DepositTransaction(account, amount);
        bank.ExecuteTransaction(depositTransaction);
        Console.WriteLine($"Deposit of ${amount} successful. New balance: {account.Balance}");
    }

    public static void DoTransfer(Bank bank)
    {
        Console.Write("Enter account name for transfer from: ");
        string? fromName = Console.ReadLine();
        if (string.IsNullOrEmpty(fromName))
        {
            Console.WriteLine("Invalid source account name.");
            return;
        }

        Account? fromAccount = bank.GetAccount(fromName);
        if (fromAccount == null)
        {
            Console.WriteLine("Source account not found.");
            return;
        }

        Console.Write("Enter account name for transfer to: ");
        string? toName = Console.ReadLine();
        if (string.IsNullOrEmpty(toName))
        {
            Console.WriteLine("Invalid destination account name.");
            return;
        }

        Account? toAccount = bank.GetAccount(toName);
        if (toAccount == null)
        {
            Console.WriteLine("Destination account not found.");
            return;
        }

        decimal amount;
        bool validAmount;
        do
        {
            Console.Write("Enter the amount to transfer: ");
            string? userInput = Console.ReadLine();
            validAmount = decimal.TryParse(userInput, out amount) && amount > 0;

            if (!validAmount)
                Console.WriteLine("Invalid amount. Please enter a valid positive number.");
        } while (!validAmount);

        TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
        try
        {
            bank.ExecuteTransaction(transferTransaction);
            Console.WriteLine($"Transfer of ${amount} successful from {fromAccount.Name} to {toAccount.Name}.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Transfer failed: {ex.Message}");
        }
    }

    public static void DoPrintAccounts(Bank bank)
    {
        Console.WriteLine("\nPrinting Account Details:");
        foreach (var account in bank.GetAccounts())
        {
            Console.WriteLine($"Account Name: {account.Name}, Balance: {account.Balance}");
        }
    }

    public static void DoPrintTransactionHistory(Bank bank)
    {
        bank.PrintTransactionHistory();
    }

    public static void DoRollback(Bank bank)
    {
        bank.PrintTransactionHistory(); // Prints the history of transactions

        Console.Write("Enter the transaction index to roll back: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index <= 0 || index > bank.GetTransactionsCount())
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        try
        {
            Transaction transaction = bank.GetTransactionByIndex(index - 1);
            bank.RollbackTransaction(transaction);
            Console.WriteLine("Transaction successfully rolled back.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error rolling back transaction: {ex.Message}");
        }
    }


    public static void DoAddAccount(Bank bank)
    {
        Console.Write("Enter account name: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid account name.");
            return;
        }

        Console.Write("Enter starting balance: ");
        decimal balance;
        while (!decimal.TryParse(Console.ReadLine(), out balance))
        {
            Console.WriteLine("Invalid input. Please enter a valid number for balance.");
            Console.Write("Enter starting balance: ");
        }

        Account account = new Account(name, balance);
        bank.AddAccount(account);
        Console.WriteLine("Account added successfully.");
    }
}
