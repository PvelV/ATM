using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICheckingAccountRepository CheckingAccounts { get; }
        ITransactionRepository Transactions { get; }

        int Complete();
    }
}
