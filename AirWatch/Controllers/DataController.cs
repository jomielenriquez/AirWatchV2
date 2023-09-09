using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml.Linq;
using AirWatch.Models;
using AirWatch.Repository;

namespace AirWatch.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpGet]
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

            //Data newdata = new Data();
            //string result = newdata.Save(data, new List<string> { "ENVIRONMENTDATEID" }, "ENVIRONMENTDATEID");

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpGet]
        public JsonResult GetOneHundred()
        {
            // will return count 0 if no data
            List<TBL_ENVIRONMENTDATA> EnvData = EnvironmentDataRepository.GetTop100();

            return Json(EnvData, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpPost]
        public JsonResult PostData([FromBody] ReadingData data)
        {
            if(data == null)
            {
                return Json(new { message = "Invalid data.", isSucess = false });
            }
            Guid? ValidAPIKey = ConfigurationRepository.GetValidAPIkey();
            try
            {
                if(new Guid(data.apikey) != ValidAPIKey)
                {
                    return Json(new { message = "Error invalid api key.", isSucess = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error invalid api key.", isSucess = false });
            }

            Data newdata = new Data();

            TBL_ENVIRONMENTDATA edata = new TBL_ENVIRONMENTDATA() { 
                HUMIDITY = data.humidity,
                AMMONIA = data.ammonia,
                SULFURDIOXICE = data.sulfurdioxide,
                TEMPERATURE = data.temperature,
                CARBONMONOXIDE = data.carbonmonoxide,
                NITROGENOXIDE = data.nitrogenoxide
            };

            string result =  newdata.Save(edata, new List<string> { "ENVIRONMENTDATEID" }, "ENVIRONMENTDATEID");

            try
            {
                Guid InsertedGuid = new Guid(result);
            }
            catch(Exception ex)
            {
                return Json(new { message = "result", isSucess = false });
            }

            return Json(new { message = "Success", isSucess = true });
        }
    }
}