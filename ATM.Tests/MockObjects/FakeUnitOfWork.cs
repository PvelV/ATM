using ATM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Tests.MockObjects
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public ICheckingAccountRepository CheckingAccounts { get; }
        public ITransactionRepository Transactions { get; }

        public FakeUnitOfWork()
        {
            CheckingAccounts = new FakeCheckingAccountRepository();
            Transactions = new FakeTransactionRepository();
        }


        public int Complete()
        {
            return 1;
        }
    }
}
