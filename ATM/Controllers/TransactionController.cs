using ATM.Models;
using ATM.Repository;
using ATM.Services;
using ATM.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private IUnitOfWork db;
        private CheckingAccountService checkingAccountService;
        private PaymentService paymentService;

        public DepositResult ResultTypes { get; private set; }

        public TransactionController(IUnitOfWork unitOfWork, CheckingAccountService service, PaymentService payment)
        {
            db = unitOfWork;
            checkingAccountService = service;
            paymentService = payment;
        }

        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Withdraw()
        {
            var userId = User.Identity.GetUserId();
            var account = db.CheckingAccounts.GetByUserId(userId);

            return View(new TransactionViewModel
            {
                Balance = account.Balance,
                Transaction = new Transaction { CheckingAccountId = account.Id, TransactionType = TransactionTypes.Withdrawal }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(Transaction transaction)
        {

            if (ModelState.IsValid)
            {
                var result = checkingAccountService.UpdateBalance(transaction);

                switch (result)
                {
                    case DepositResult.OK:
                        return RedirectToAction("Index", "Home");

                    case DepositResult.InsufficientFunds:
                        ModelState.AddModelError("Transaction.Amount", "Insufficient funds.");
                        break;
                    case DepositResult.AccountIdNotExistent:
                        ModelState.AddModelError("Transaction.CheckingAccountId", "Account doesnt exist.");
                        break;
                    case DepositResult.ERR:
                        break;
                    default:
                        break;
                }

            }
            return View(new TransactionViewModel { Balance = db.CheckingAccounts.Get(transaction.CheckingAccountId).Balance, Transaction = transaction });
        }


        [HttpGet]
        public ActionResult Deposit()
        {
            var userId = User.Identity.GetUserId();
            var account = db.CheckingAccounts.GetByUserId(userId);

            return View(new TransactionViewModel
            {
                Balance = account.Balance,
                Transaction = new Transaction { CheckingAccountId = account.Id, TransactionType = TransactionTypes.Deposit }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var result = checkingAccountService.UpdateBalance(transaction);

                if (result == DepositResult.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(new TransactionViewModel
            {
                Balance = db.CheckingAccounts.Get(transaction.CheckingAccountId).Balance,
                Transaction = transaction
            });
        }


        [HttpGet]
        public ActionResult TransferFunds()
        {
            var userId = User.Identity.GetUserId();
            var account = db.CheckingAccounts.GetByUserId(userId);


            return View(new PaymentViewModel
            {
                Balance = account.Balance,
                Transaction = new Transaction { CheckingAccountId = account.Id, TransactionType = TransactionTypes.Withdrawal },
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransferFunds(PaymentViewModel paymentVM)
        {
            if (ModelState.IsValid)
            {
                var recipientAccountId = db.CheckingAccounts.GetByAccountNumber(paymentVM.RecipientAccountNumber).Id;

                var payment = new Payment
                {
                    Amount = paymentVM.Transaction.Amount,
                    RecipientCheckingAccountId = recipientAccountId,
                    SenderCheckingAccountId = paymentVM.Transaction.CheckingAccountId
                };

                var result = paymentService.SettlePayment(payment);

                switch (result)
                {
                    case DepositResult.OK:
                        return RedirectToAction("Index", "Home");

                    case DepositResult.InsufficientFunds:
                        ModelState.AddModelError("Transaction.Amount", "Insufficient funds.");
                        break;
                    case DepositResult.AccountIdNotExistent:
                        ModelState.AddModelError("Transaction.CheckingAccountId", "Account doesnt exist.");
                        break;
                    case DepositResult.ERR:
                        break;
                    default:
                        break;
                }

            }
            return View(paymentVM);
        }
    }
}