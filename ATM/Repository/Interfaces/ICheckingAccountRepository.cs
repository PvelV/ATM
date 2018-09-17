using ATM.Models;

namespace ATM.Repository
{
    public interface ICheckingAccountRepository : IRepository<CheckingAccount>
    {
        CheckingAccount GetByUserId(string id);
        CheckingAccount GetByAccountNumber(string accountNumber);
    }
}