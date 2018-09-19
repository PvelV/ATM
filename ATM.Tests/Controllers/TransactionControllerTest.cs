using ATM.Controllers;
using ATM.Models;
using ATM.Repository;
using ATM.Services;
using ATM.Tests.MockObjects;
using ATM.ViewModels;
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
            checkingAccountService = new CheckingAccountService(db);
        }



        [Test]
        public void Deposit_Controller_OK()
        {
            var account = new CheckingAccount { Id = 1, Balance = 0, AccountNumber = "0000" };
            db.CheckingAccounts.Add(account);

            var transaction = new Transaction
            {
                Amount = 200,
                CheckingAccountId = account.Id,
                TransactionType = TransactionTypes.Deposit
            };

            var transactionController = new TransactionController(db, new CheckingAccountService(db), new PaymentService(db));
            transactionController.Deposit(transaction);

            Assert.True(transactionController.ModelState.IsValid);
            Assert.AreEqual(transaction.Amount, account.Balance);
        }


        [Test]
        public void Withdrawal_Controller()
        {

            var account = new CheckingAccount { Id = 1, Balance = 200, AccountNumber = "0000" };
            db.CheckingAccounts.Add(account);

            var transaction = new Transaction
            {
                Amount = 100,
                CheckingAccountId = account.Id,
                TransactionType = TransactionTypes.Withdrawal
            };

            var transactionController = new TransactionController(db, new CheckingAccountService(db), new PaymentService(db));
            transactionController.Withdraw(transaction);
            Assert.AreEqual(100, account.Balance);

        }

        [Test]
        public void TransferFunds_Controller()
        {

            var accountSender = new CheckingAccount { Id = 1, Balance = 200, AccountNumber = "0000" };
            var accountRecipient = new CheckingAccount { Id = 2, Balance = 200, AccountNumber = "0001" };
            db.CheckingAccounts.Add(accountSender);
            db.CheckingAccounts.Add(accountRecipient);

            

            var transactionController = new TransactionController(db, new CheckingAccountService(db), new PaymentService(db));

            var paymentVM = new PaymentViewModel
            {
                RecipientAccountNumber = accountRecipient.AccountNumber,
                Balance = accountSender.Balance,
                SenderAccountId = accountSender.Id,
                Amount = 100
            };

            transactionController.TransferFunds(paymentVM);

            Assert.That(accountRecipient.Balance == 300);
            Assert.That(accountSender.Balance == 100);

        }
    }
}
