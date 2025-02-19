# Bank-Management-System-C-.NET-
This is a console-based banking system developed in C# using object-oriented principles. It simulates basic banking operations with the ability to manage multiple accounts, track transactions, and even roll back transactions to ensure data integrity. This project demonstrates my understanding of C#, .NET, and fundamental object-oriented design concepts.

Features

Withdraw: Allows users to withdraw money from their accounts with checks for sufficient funds.
Deposit: Users can deposit money into their accounts.
Transfer: Enables transferring funds between multiple accounts.
Print Accounts: Displays a list of all accounts with their current balance.
Transaction History: Prints all transactions related to an account for tracking.
Rollback Transaction: A fully functioning feature that lets users undo the most recent transaction, ensuring flexibility and data consistency.
Add Account: Enables the creation of new user accounts with initial deposit and account details.


Technologies Used
Language: C#
Framework: .NET (Core)
Concepts: Object-Oriented Programming (OOP), Data Integrity, Transaction Management, and Rollback Mechanism


How It Works
The system is driven by a simple text-based user interface (UI), where users can select different options to interact with their accounts.
The rollback feature is implemented by storing transaction details in memory and allowing the user to reverse the last performed action.
Multiple accounts are stored and managed within a list, with each account containing its own set of transactions.


Future Improvements
Implementing a database for persistent storage of accounts and transactions.
Adding a user authentication feature for secure login.
Expanding to a full GUI for easier interaction.
