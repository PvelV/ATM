using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public ICheckingAccountRepository CheckingAccounts { get; private set; }
        public ITransactionRepository Transactions { get; private set; }


        public UnitOfWork(ApplicationDbContext _db)
        {
            db = _db;

            CheckingAccounts = new CheckingAccountRepository(_db.CheckingAccounts);
            Transactions = new TransactionRepository(_db.Transactions);

        }


        public int Complete()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
