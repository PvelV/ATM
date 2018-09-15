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

        public DepositResult SettlePayment(PaymentViewModel payment)
        {
            return DepositResult.ERR;
        }
    }
}