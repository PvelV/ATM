using ATM.Models;
using ATM.Repository;
using ATM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Services
{

    public class PaymentService
    {
        private IUnitOfWork db;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }


        public DepositResult SettlePayment(Payment payment)
        {
            var senderAccount = db.CheckingAccounts.Get(payment.SenderCheckingAccountId);
            var recipientAccount = db.CheckingAccounts.Get(payment.RecipientCheckingAccountId);

            if (recipientAccount == null)
                return DepositResult.AccountIdNotExistent;

            if (senderAccount.Balance < payment.Amount)
                return DepositResult.InsufficientFunds;

            senderAccount.Balance -= payment.Amount;
            recipientAccount.Balance += payment.Amount;

            db.Complete();

            return DepositResult.OK;
        }
    }
}