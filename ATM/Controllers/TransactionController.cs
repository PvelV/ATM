using ATM.Models;
using ATM.Repository;
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

        public TransactionController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var checkingAccountId = db.CheckingAccounts.GetByUserId(userId).Id;

                transaction.CheckingAccountId = checkingAccountId;

                db.Transactions.Add(transaction);
                db.Complete();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}