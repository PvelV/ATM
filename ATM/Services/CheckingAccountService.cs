using ATM.Models;
using ATM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Services
{
    public enum DepositResult { OK, InsufficientFunds, AccountIdNotExistent, ERR }

    public class CheckingAccountService
    {
        private IUnitOfWork db;

        public CheckingAccountService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance)
        {
            var checkingAccount = new CheckingAccount
            {
                FirstName = firstName,
                LastName = lastName,
                AccountNumber = (123456 + db.CheckingAccounts.Count() + 5).ToString().PadLeft(10, '0'),
                Balance = initialBalance,
                ApplicationUserId = userId
            };
            db.CheckingAccounts.Add(checkingAccount);
            db.Complete();
        }

        public DepositResult UpdateBalance(Transaction transaction)
        {
            switch (transaction.TransactionType)
            {
                case TransactionTypes.Withdrawal:
                    return Withdraw(transaction);

                case TransactionTypes.Deposit:
                    return Deposit(transaction);
                    
            }
            return DepositResult.ERR;
        }

        private DepositResult Withdraw(Transaction transaction)
        {
            var account = db.CheckingAccounts.Get(transaction.CheckingAccountId);
            if (account == null) return DepositResult.AccountIdNotExistent;

            if (account.Balance < transaction.Amount) return DepositResult.InsufficientFunds;
            account.Balance -= transaction.Amount;
            db.Transactions.Add(transaction);
            db.Complete();

            return DepositResult.OK;
        }

        private DepositResult Deposit(Transaction transaction)
        {
            var account = db.CheckingAccounts.Get(transaction.CheckingAccountId);
            if (account == null) return DepositResult.AccountIdNotExistent;

            account.Balance += transaction.Amount;
            db.Transactions.Add(transaction);
            db.Complete();

            return DepositResult.OK;
        }
    }
}