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
using Unity;

namespace ATM.Tests.Controllers
{
    [TestFixture]
    public class CheckingAccountControllerTest
    {

        private IUnitOfWork db;
        
        [SetUp]
        public void Setup()
        {
            db = new FakeUnitOfWork();

        }
        

        [Test]
        public void BalanceIsCorrectAfterDeposit()
        {
            var service = new CheckingAccountService(db);

            service.CreateCheckingAccount("testFN", "testLN", "0", 10);

            Assert.That(db.CheckingAccounts.Count() == 1);
            Assert.That(db.CheckingAccounts.GetByUserId("0").Balance == 10);
        }
    }
}
