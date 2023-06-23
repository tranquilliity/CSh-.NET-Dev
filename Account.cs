using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankomatAccounts
{
    public class Account
    {
        private string accountNumber; // номер рахунку клієнта
        private string password; // пароль рахунку клієнта
        private double balance; // поточний баланс рахунку клієнта
        public Account() : this("no set", "no set", 0) {}
        public Account(string accountNumber, string password, double balance)
        {
            this.accountNumber = accountNumber;
            this.password = password;
            this.balance = balance;
        }
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public double Balance
        {
            get { return balance; } 
            set { balance = value; }
        }
        public void Deposit(double amount) { this.balance += amount; }
        public void Withdraw(double amount) { this.balance -= amount; }


    }
}
