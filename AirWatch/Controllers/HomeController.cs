using AirWatch.Models;
using AirWatch.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Http;

namespace AirWatch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // This code will return user to the login page if there is no session
            AppModel appModel = new AppModel();
            if (appModel.Account == null)
            {
                return RedirectToAction("../Login/SignIn");
            }

            return View(appModel);
        }

        public ActionResult DowloadReports(AppModel model)
        {
            // This code will return user to the login page if there is no session
            if (model == null)
            {
                model = new AppModel();
            }
            AppModel appModel = model;
            if (appModel.Account == null)
            {
                return RedirectToAction("../Login/SignIn");
            }

            List<TBL_ENVIRONMENTDATA> envData = EnvironmentDataRepository.GetDataByDate(DateTime.Parse(model.DateFilter));
            appModel.DateFilter = DateTime.Now.ToString("yyyy-MM-dd");
            appModel.Data = envData;
            return View(appModel);
        }

        public ActionResult Logs(AppModel model)
        {
            // This code will return user to the login page if there is no session
            if(model == null)
            {
                model = new AppModel();
            }
            AppModel appModel = model;
            if (appModel.Account == null)
            {
                return RedirectToAction("../Login/SignIn");
            }

            List<TBL_ENVIRONMENTDATA> envData = EnvironmentDataRepository.GetDataByDate(DateTime.Parse(model.DateFilter));
            appModel.DateFilter = DateTime.Now.ToString("yyyy-MM-dd");
            appModel.Data = envData;
            return View(appModel);
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