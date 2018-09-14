using ATM.Controllers;
using ATM.Models;
using ATM.Repository;
using ATM.Services;
using ATM.Tests.MockObjects;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Unity;

namespace ATM.Tests.Controllers
{
    [TestFixture]
    public class TransactionControllerTest
    {

        private IUnitOfWork db;
        private CheckingAccountService checkingAccountService;

        
        [SetUp]
        public void Setup()
        {
            db = new FakeUnitOfWork();
            checkingAccountService =new CheckingAccountService(db);
        }
        


        [Test]
        public void Deposit_Controller_OK()
        {
            var account = new CheckingAccount { Id = 1, Balance = 0, AccountNumber = "0000" };
            db.CheckingAccounts.Add(account);

            var transaction = new Transaction { Amount = 200, CheckingAccountId = account.Id,
                TransactionType = TransactionTypes.Deposit };

            var transactionController = new TransactionController(db, new CheckingAccountService(db));
            transactionController.Deposit(transaction);

            Assert.True(transactionController.ModelState.IsValid);
            Assert.AreEqual(transaction.Amount, account.Balance);
        }


        [Test]
        public void Withdrawal_Controller()
        {

            var account = new CheckingAccount { Id = 1, Balance = 200, AccountNumber = "0000" };
            db.CheckingAccounts.Add(account);

            var transaction = new Transaction { Amount = 100, CheckingAccountId = account.Id,
                TransactionType = TransactionTypes.Withdrawal };

            var transactionController = new TransactionController(db, new CheckingAccountService(db));
            transactionController.Withdraw(transaction);
            Assert.AreEqual(100, account.Balance);

        }
    }
}
