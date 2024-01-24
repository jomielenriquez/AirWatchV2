using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml.Linq;
using AirWatch.Models;
using AirWatch.Repository;
using System.Net.Mail;
using System.Net;
using System.Web.Services.Description;

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

            //Random rand = new Random();

            //data.HUMIDITY = rand.Next(0 , 70);
            //data.AMMONIA = rand.Next(0, 9);
            //data.SULFURDIOXICE = rand.Next(440, 530);
            //data.TEMPERATURE = rand.Next(17, 40);
            //data.CARBONMONOXIDE = rand.Next(0, 2000);
            //data.NITROGENOXIDE = rand.Next(0, 90);
            //// Specify the desired time zone (Taipei Standard Time)
            //TimeZoneInfo taipeiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");

            //// Get the current UTC time
            //DateTime currentUtcTime = DateTime.UtcNow;

            //// Convert the UTC time to Taipei time
            //DateTime taipeiTime = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, taipeiTimeZone);

            //data.CREATEDDATE = taipeiTime;

            data = EnvironmentDataRepository.GetLatest();

            //Data newdata = new Data();
            //string result = newdata.Save(data, new List<string> { "ENVIRONMENTDATEID" }, "ENVIRONMENTDATEID");
            var jsonData = new TBL_ENVIRONMENTDATAOUT()
            {
                ENVIRONMENTDATEID = data.ENVIRONMENTDATEID,
                EDID = data.EDID,
                HUMIDITY = data.HUMIDITY,
                AMMONIA = data.AMMONIA,
                SULFURDIOXICE = data.SULFURDIOXICE,
                TEMPERATURE = data.TEMPERATURE,
                CARBONMONOXIDE = data.CARBONMONOXIDE,
                NITROGENOXIDE = data.NITROGENOXIDE,
                ISDISPLAYED = data.ISDISPLAYED,
                CREATEDDATE = data.CREATEDDATE.ToString("yyyy-MM-ddTHH:mm:ss"), // Convert to string in the desired format
                CREATEDBY = data.CREATEDBY,
                UPDATEDDATE = data.UPDATEDDATE,
                UPDATEDBY = data.UPDATEDBY,
                SO2CONCENTRATION = data.SO2CONCENTRATION,
                COCONCENTRATION = data.COCONCENTRATION,
                NOXCONCENTRATION = data.NOXCONCENTRATION,
                AQI = data.AQI,
                AQICATEGORY = data.AQICATEGORY
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Http.HttpGet]
        public JsonResult GetOneHundred()
        {
            // will return count 0 if no data
            List<TBL_ENVIRONMENTDATAOUT> EnvData = EnvironmentDataRepository.GetTop100();

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

            Decimal SO2 = Decimal.Parse("0.2") * decimal.Parse(data.sulfurdioxide.ToString());
            Decimal CO = Decimal.Parse("10") * decimal.Parse(data.carbonmonoxide.ToString());
            Decimal NOX = Decimal.Parse("0.5") * decimal.Parse(data.nitrogenoxide.ToString());
            Decimal AQI = SO2 > CO ? SO2 > NOX ? SO2 : NOX : CO > NOX ? CO : NOX;
            string AQICategory = AQI >= 0 && AQI <= 50
                ? "Good"
                : AQI >= 51 && AQI <= 100
                    ? "Satisfactory"
                    : AQI >= 101 && AQI <= 200
                        ? "Moderately Polluted"
                        : AQI >= 201 && AQI <= 300
                            ? "Poor"
                            : AQI >= 301 && AQI <= 400
                                ? "Very Poor"
                                : AQI >= 401 && AQI <= 500
                                    ? "Severe"
                                : "Error";
            TimeZoneInfo taipeiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");

            //DateTime gmtPlus1Time = DateTime.Now; // Replace with your GMT+1 time

            //// Specify the time zone for GMT+1
            //TimeZoneInfo gmtPlus1TimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

            //// Convert GMT+1 time to UTC
            //DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(gmtPlus1Time, gmtPlus1TimeZone);

            //// Specify the time zone for UTC+8 (Taipei)
            //TimeZoneInfo utcPlus8TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");

            //// Convert UTC time to UTC+8
            //DateTime utcPlus8Time = TimeZoneInfo.ConvertTimeFromUtc(utcTime, utcPlus8TimeZone);

            var latestRecord = EnvironmentDataRepository.GetLatest();

            TBL_ENVIRONMENTDATA edata = new TBL_ENVIRONMENTDATA() {
                HUMIDITY = data.humidity,
                AMMONIA = data.ammonia,
                SULFURDIOXICE = data.sulfurdioxide,
                TEMPERATURE = data.temperature,
                CARBONMONOXIDE = data.carbonmonoxide,
                NITROGENOXIDE = data.nitrogenoxide,
                SO2CONCENTRATION = SO2,
                COCONCENTRATION = CO,
                NOXCONCENTRATION = NOX,
                AQI = AQI,
                AQICATEGORY = AQICategory,
                CREATEDDATE = TimeZoneInfo.ConvertTime(DateTime.Now, taipeiTimeZone)
                //CREATEDDATE = utcPlus8Time
            };

            var humidityLimit = 100;
            var ammoniaLimit = 450;
            var sulfurLimit = 50;
            var temperatureLimit = 100;
            var carbonMonoxideLimit = 50;
            var nitrogenOxideLimit = 50;

            List<PollutantReading> PollutantList = new List<PollutantReading>();

            if (edata.AMMONIA > ammoniaLimit)
            {
                PollutantList.Add(new PollutantReading()
                {
                    Pollutant = "Ammonia",
                    Level = "High",
                    Value = $"{edata.AMMONIA} "

                });
            }
            if (edata.CARBONMONOXIDE > carbonMonoxideLimit)
            {
                PollutantList.Add(new PollutantReading()
                {
                    Pollutant = "Carbon Monoxide",
                    Level = "High",
                    Value = $"{edata.CARBONMONOXIDE * 169} ug/m^3"

                });
            }
            if (edata.NITROGENOXIDE > nitrogenOxideLimit)
            {
                PollutantList.Add(new PollutantReading()
                {
                    Pollutant = "Nitrogen Oxide",
                    Level = "High",
                    Value = $"{edata.NITROGENOXIDE} ug/m^3"

                });
            }
            if (edata.SULFURDIOXICE > sulfurLimit)
            {
                PollutantList.Add(new PollutantReading()
                {
                    Pollutant = "Sulfur Dioxide",
                    Level = "High",
                    Value = $"{edata.SULFURDIOXICE * 4} ug/m^3"

                });
            }

            if (PollutantList.Count > 0)
            {
                SendEmail(edata.CREATEDDATE, PollutantList);
            }
            
            if (latestRecord.HUMIDITY != edata.HUMIDITY || latestRecord.AMMONIA != edata.AMMONIA || latestRecord.SULFURDIOXICE != edata.SULFURDIOXICE || latestRecord.TEMPERATURE != edata.TEMPERATURE || latestRecord.CARBONMONOXIDE != edata.CARBONMONOXIDE || latestRecord.NITROGENOXIDE != edata.NITROGENOXIDE || latestRecord.SO2CONCENTRATION != edata.SO2CONCENTRATION || latestRecord.COCONCENTRATION != edata.COCONCENTRATION || latestRecord.NOXCONCENTRATION != edata.NOXCONCENTRATION)
            {
                string result =  newdata.Save(edata, new List<string> { "ENVIRONMENTDATEID" }, "ENVIRONMENTDATEID");
                try
                {
                    Guid InsertedGuid = new Guid(result);
                }
                catch(Exception ex)
                {
                    return Json(new { message = "result", isSucess = false });
                }
            }


            return Json(new { message = "Success", isSucess = true });
        }

        public string SendEmail(DateTime dateTime, List<PollutantReading> PollutantList)
        {
            // Set your SMTP server and credentials
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587; // This may vary depending on your SMTP server
            string smtpUsername = "airwatch623@gmail.com";
            string smtpPassword = "ngyk qcfi cnib bzdz";

            // Create a new instance of SmtpClient
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);

            // Set the credentials for authentication
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            // Enable SSL if required by your SMTP server
            smtpClient.EnableSsl = true;

            // Create a MailMessage object
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("airwatch623@gmail.com");
            mailMessage.To.Add("airwatch623@gmail.com");
            mailMessage.Subject = "Air Pollution Detected";
            mailMessage.Body = $"Reading as of: {dateTime} \n\n<br><br><table><tr><th>Pollutants</th><th>Level</th><th>Measured Value</th></tr>";

            foreach(var poll in PollutantList)
            {
                mailMessage.Body += $"<tr><td>{poll.Pollutant}</td><td>{poll.Level}</td><td>{poll.Value}</td></tr>";
            }

            mailMessage.Body += "</table>\n\n<br><br>Warning! High Level of air pollution has been detected in your area.";

            mailMessage.IsBodyHtml = true;

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                return "Email sent successfully.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}