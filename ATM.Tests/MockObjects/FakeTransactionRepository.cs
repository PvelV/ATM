using ATM.Models;
using ATM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Tests.MockObjects
{
    public class FakeTransactionRepository : FakeRepository<Transaction>, ITransactionRepository
    {
        public IEnumerable<Transaction> GetAllTransactionByAccount(int accountId)
        {
            return dbSet.Where(t => t.CheckingAccountId == accountId);
        }
    }
}
