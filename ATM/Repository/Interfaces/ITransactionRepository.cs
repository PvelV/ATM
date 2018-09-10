using ATM.Models;
using System.Collections;
using System.Collections.Generic;

namespace ATM.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetAllTransactionByAccount(int accountId);
    }
}