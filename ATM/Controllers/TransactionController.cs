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
                checkingAccountService.UpdateBalance(transaction);

                db.Complete();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}