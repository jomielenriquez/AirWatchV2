using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirWatch.Models;

namespace AirWatch.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetData()
        {
            TBL_ENVIRONMENTDATA data = new TBL_ENVIRONMENTDATA();

            Random rand = new Random();

            data.HUMIDITY = rand.Next(0 , 70);
            data.AMMONIA = rand.Next(0, 9);
            data.SULFURDIOXICE = rand.Next(440, 530);
            data.TEMPERATURE = rand.Next(17, 40);
            data.CARBONMONOXIDE = rand.Next(0, 2000);
            data.NITROGENOXIDE = rand.Next(0, 90);
            // Specify the desired time zone (Taipei Standard Time)
            TimeZoneInfo taipeiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");

            // Get the current UTC time
            DateTime currentUtcTime = DateTime.UtcNow;

            // Convert the UTC time to Taipei time
            DateTime taipeiTime = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, taipeiTimeZone);

            data.CREATEDDATE = taipeiTime;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}