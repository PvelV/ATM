using ATM.Models;
using ATM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Tests.MockObjects
{
    public class FakeCheckingAccountRepository : FakeRepository<CheckingAccount>, ICheckingAccountRepository
    {
      
        public CheckingAccount GetByUserId(string id)
        {
            return dbSet.Where(c => c.ApplicationUserId == id).FirstOrDefault();
        }
    }
}
