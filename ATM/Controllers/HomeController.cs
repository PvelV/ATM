using ATM.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    public class HomeController : Controller
    {
       // private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.TheMessage = "Thanks!";
            return View();
        }

        [HttpPost]
        public ActionResult Message(string message)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.TheMessage = message += "  Thanks we got your message";

            return PartialView("_Message");
        }

        public ActionResult Foo()
        {
            return View("About");
        }

        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPNETMVCATM";

            if (letterCase=="lower")
            {
                return Content(serial.ToLower());
            }
            return Content(serial);
        }
    }
}