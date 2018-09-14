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
    public class CheckingAccountController : Controller
    {
        private IUnitOfWork db;

        public CheckingAccountController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        // GET: CheckingAccount
        public ActionResult Index()
        {
            return View();
        }

        // GET: CheckingAccount/Details
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var account = db.CheckingAccounts.GetByUserId(userId);
            return View(account);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DetailsForAdmin(int id)
        {
            var account = db.CheckingAccounts.Get(id);
            return View("Details", account);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            return View(db.CheckingAccounts.GetAll());
        }

        public ActionResult Statement()
        {
            var userId = User.Identity.GetUserId();
            var account = db.CheckingAccounts.GetByUserId(userId);

            var transactions = db.Transactions.GetAllTransactionByAccount(account.Id).OrderByDescending(t => t.Id);

            return View(transactions);
        }
    }
}
