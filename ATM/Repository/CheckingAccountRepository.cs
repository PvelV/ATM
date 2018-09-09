using ATM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ATM.Repository
{
    public class CheckingAccountRepository : Repository<CheckingAccount>, ICheckingAccountRepository
    {
        public CheckingAccountRepository(DbSet<CheckingAccount> _dbSet) : base(_dbSet)
        {
        }

        public CheckingAccount GetByUserId(string id)
        {
            return dbSet.Where(c => c.ApplicationUserId == id).FirstOrDefault();
        }
    }
}