using System;
using BankomatClients;
using BankomatAccounts;
using Bankomat;

Bank bank = new Bank();
bank.OpenAccount("a012345");
Console.WriteLine(new string('-', 50));
bank.OpenAccount("a12345");
Console.WriteLine(new string('-', 50));
bank.OpenAccount("a23456");

while (true)
{
    Console.WriteLine($"{new string('-', 50)}\nWelcome to the ATM!");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Open an account");
    Console.WriteLine("2. Log in");
    Console.Write("\nEnter your choice: ");
    int option;
    try
    {
        option = Convert.ToInt32(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Please try again.");
        continue;
    }
    Console.WriteLine(new string('-', 50));
    switch (option)
    {
        case 1:
            Console.Clear();
            bank.OpenAccount();
            break;
        case 2:
            Console.Clear();
            Console.Write("Enter your card number: ");
            string cardNumber = Console.ReadLine();
            Client client = new Client(cardNumber);
            if (!client.CheckAccount(cardNumber, bank))
            {
                Console.WriteLine("Account not found.");
                break;
            }
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            int attempts = 3;
            while (!client.CheckPassword(cardNumber, password, bank))
            {
                attempts--;
                Console.WriteLine($"Invalid password. Attempts remaining: {attempts}\n");
                if (attempts == 0)
                {
                    Console.WriteLine("Maximum attempts reached. Returning to the main menu.");
                    break;
                }
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
            }

            if (attempts == 0)
                break;

            Console.WriteLine("\nLogin successful. Welcome!");
            bool loggedIn = true;

            while (loggedIn)
            {
                int ind = client.getInd(cardNumber, password, bank);
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("MENU:");
                Console.WriteLine("0. Log out");
                Console.WriteLine("1. Check account balance");
                Console.WriteLine("2. Deposit money");
                Console.WriteLine("3. Withdraw money");
                Console.Write("Enter your choice: ");
                int menuOption;
                try
                {
                    menuOption = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.WriteLine();
                    continue;
                }
                Console.WriteLine(new string('-', 50));

                string accountNumber = bank.GetAccountNumber(ind);

                switch (menuOption)
                {
                    case 1:
                        Console.Clear();
                        string userAccountNumber = bank.GetAccountNumber(ind);
                        double userBalance = bank.Accounts.Find(account => account.AccountNumber == userAccountNumber).Balance;
                        Console.WriteLine($"Balance: {userBalance}");
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Enter the amount to deposit: ");
                        double depositAmount;
                        if (!double.TryParse(Console.ReadLine(), out depositAmount))
                        {
                            Console.WriteLine("Invalid input! Please try again.");
                            break;
                        }

                        bank.Deposit(ind, depositAmount);
                        Console.WriteLine("Deposit successful!");
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Enter the amount to withdraw: ");
                        double withdrawAmount;
                        if (!double.TryParse(Console.ReadLine(), out withdrawAmount))
                        {
                            Console.WriteLine("Invalid input! Please try again.");
                            break;
                        }

                        userAccountNumber = bank.GetAccountNumber(ind); 
                        userBalance = bank.Accounts.Find(account => account.AccountNumber == userAccountNumber).Balance;
                        if (withdrawAmount > userBalance)
                        {
                            Console.WriteLine("Insufficient funds!");
                            break;
                        }

                        bank.Withdraw(ind, withdrawAmount); 
                        Console.WriteLine("Withdrawal successful!");
                        break;
                    case 0:
                        Console.Clear();
                        loggedIn = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
            break;
        case 0:
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}
