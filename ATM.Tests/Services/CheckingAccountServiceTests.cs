using ATM.Models;
using ATM.Repository;
using ATM.Services;
using ATM.Tests.MockObjects;
using NUnit.Framework;
using System;

namespace ATM.Tests.Services
{
    [TestFixture]
    public class CheckingAccountServiceTests
    {
        private IUnitOfWork db;
        private CheckingAccountService checkingAccountService;


        [SetUp]
        public void Setup()
        {
            db = new FakeUnitOfWork();
            checkingAccountService = new CheckingAccountService(db);
        }





        [Test]
        public void BalanceIsCorrectAfterCreation()
        {

            checkingAccountService.CreateCheckingAccount("testFN", "testLN", "0", 10);

            Assert.That(db.CheckingAccounts.Count() == 1);
            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 10);
        }

        [Test]
        public void BalanceIsCorrectAfterWithdrawal()
        {

            checkingAccountService.CreateCheckingAccount("testFN", "testLN", "0", 25);

            var transaction = new Transaction { Amount = 10, TransactionType = TransactionTypes.Withdrawal, CheckingAccountId = 0 };

            checkingAccountService.UpdateBalance(transaction);

            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 15);
        }


        [Test]
        public void BalanceIsCorrectAfterDeposit()
        {

            checkingAccountService.CreateCheckingAccount("testFN", "testLN", "0", 0);

            var transaction = new Transaction { Amount = 10, TransactionType = TransactionTypes.Deposit, CheckingAccountId = 0 };

            checkingAccountService.UpdateBalance(transaction);

            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 10);
        }

        [Test]
        public void Withdrawal_InsufficientFunds()
        {
            checkingAccountService.CreateCheckingAccount("testFN", "testLN", "0", 0);

            var transaction = new Transaction { Amount = 10, TransactionType = TransactionTypes.Withdrawal, CheckingAccountId = 0 };

            Assert.That(checkingAccountService.UpdateBalance(transaction) == DepositResult.InsufficientFunds);
        }

        [Test]
        public void Withdrawal_AccountIdNonExistent()
        {
            checkingAccountService.CreateCheckingAccount("testFN", "testLN", "0", 0);

            var transaction = new Transaction { Amount = 10, TransactionType = TransactionTypes.Withdrawal, CheckingAccountId = 10 };

            Assert.That(checkingAccountService.UpdateBalance(transaction) == DepositResult.AccountIdNotExistent);
        }


        [Test]
        public void Deposit_AccountIdNonExistent()
        {
            checkingAccountService.CreateCheckingAccount("testFN", "testLN", "0", 0);

            var transaction = new Transaction { Amount = 10, TransactionType = TransactionTypes.Deposit, CheckingAccountId = 10 };

            Assert.That(checkingAccountService.UpdateBalance(transaction) == DepositResult.AccountIdNotExistent);
        }
    }
}
