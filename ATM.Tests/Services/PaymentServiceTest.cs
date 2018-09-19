using System;
using ATM.Models;
using ATM.Repository;
using ATM.Services;
using ATM.Tests.MockObjects;
using NUnit.Framework;

namespace ATM.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTest
    {
        private IUnitOfWork db;
        private PaymentService paymentService;
        private CheckingAccountService checkingAccountService;

        [SetUp]
        public void Setup()
        {
            db = new FakeUnitOfWork();
            paymentService = new PaymentService(db);
            checkingAccountService = new CheckingAccountService(db);
        }

        [Test]
        public void PaymentProcessed_Success()
        {
            checkingAccountService.CreateCheckingAccount("testFN1", "testLN1", "0", 10);
            checkingAccountService.CreateCheckingAccount("testFN2", "testLN2", "1", 10);

            var payment = new Payment { Amount = 2, SenderCheckingAccountId = 0, RecipientCheckingAccountNumber = db.CheckingAccounts.GetByUserId("1").AccountNumber };

            var result = paymentService.SettlePayment(payment);

            Assert.That(result == DepositResult.OK);
            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 8 );
            Assert.That(db.CheckingAccounts.GetByUserId("1").Balance == 12);
        }

        [Test]
        public void PaymentProcessed_InsufficientFunds()
        {
            checkingAccountService.CreateCheckingAccount("testFN1", "testLN1", "0", 10);
            checkingAccountService.CreateCheckingAccount("testFN2", "testLN2", "1", 10);



            var payment = new Payment { Amount = 20, SenderCheckingAccountId = 0, RecipientCheckingAccountNumber = db.CheckingAccounts.GetByUserId("1").AccountNumber };

            var result = paymentService.SettlePayment(payment);

            Assert.That(result == DepositResult.InsufficientFunds);
            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 10);
            Assert.That(db.CheckingAccounts.GetByUserId("1").Balance == 10);
        }

        [Test]
        public void PaymentProcessed_InvalidRecipientAccountId()
        {
            checkingAccountService.CreateCheckingAccount("testFN1", "testLN1", "0", 10);
            checkingAccountService.CreateCheckingAccount("testFN2", "testLN2", "1", 10);
            
            var payment = new Payment { Amount = 2, SenderCheckingAccountId = 0, RecipientCheckingAccountNumber = db.CheckingAccounts.GetByUserId("1").AccountNumber+"5" };

            var result = paymentService.SettlePayment(payment);

            Assert.That(result == DepositResult.AccountIdNotExistent);
            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 10);
            Assert.That(db.CheckingAccounts.GetByUserId("1").Balance == 10);
        }
    }
}
