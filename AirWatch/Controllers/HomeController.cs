using AirWatch.Models;
using AirWatch.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirWatch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // This code will return user to the login page if there is no session
            TBL_ACCOUNTS account = Session["currentUserAccount"] as TBL_ACCOUNTS;
            if (account == null)
            {
                return RedirectToAction("../Login/SignIn");
            }

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

            return View();
        }
    }
}