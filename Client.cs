using BankomatAccounts;
using Bankomat;
using System;

namespace BankomatClients
{
    public class Client
    {
        private string cardNumber; // номер кредитної картки клієнта
        int id;

        public Client(string cardNumber) { this.cardNumber = cardNumber; }
        public Client() : this("no set") { }
        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public bool CheckAccount(string cardNumber, Bank bank)
        {
            foreach (Account account in bank.Accounts)
                if (account.AccountNumber == cardNumber) return true; // Знайдено відповідний обліковий запис
            return false; // Обліковий запис не знайдено
        }
        public bool CheckPassword(string cardNumber, string password, Bank bank)
        {
            foreach (Account account in bank.Accounts)
                if (account.AccountNumber == cardNumber && account.Password == password) return true; // Знайдено відповідний обліковий запис
            return false; // Обліковий запис не знайдено
        }
        public int getInd(string cardNumber, string password, Bank bank)
        {
            for (int i = 0; i < bank.Accounts.Count; i++)
            {
                Account account = bank.Accounts[i];
                if (account.AccountNumber == cardNumber && account.Password == password)
                    return i;
            }
            return -1; // Обліковий запис не знайдено
        }
    }
}
