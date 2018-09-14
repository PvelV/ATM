using ATM.Models;
using ATM.Repository;
using ATM.Services;
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

        public DepositResult ResultTypes { get; private set; }

        public TransactionController(IUnitOfWork unitOfWork, CheckingAccountService service)
        {
            db = unitOfWork;
            checkingAccountService = service;
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
            var checkingAccountId = db.CheckingAccounts.GetByUserId(userId).Id;

            return View(new Transaction { CheckingAccountId = checkingAccountId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(Transaction transaction)
        {
            var account = db.CheckingAccounts.Get(transaction.CheckingAccountId);

            if (ModelState.IsValid)
            {
                var result = checkingAccountService.UpdateBalance(transaction);

                switch (result) 
                {
                    case DepositResult.OK:
                        return RedirectToAction("Index", "Home");

                    case DepositResult.InsufficientFunds:
                        ModelState.AddModelError("Amount", "Insufficient funds.");
                        break;
                    case DepositResult.AccountIdNotExistent:
                        ModelState.AddModelError("CheckingAccountId", "Account doesnt exist.");
                        break;
                    case DepositResult.ERR:
                        break;
                    default:
                        break;
                }

            }

            return View(transaction);
        }


        [HttpGet]
        public ActionResult Deposit()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.GetByUserId(userId).Id;

            return View(new Transaction { CheckingAccountId = checkingAccountId });
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
            return View();
        }
    }
}