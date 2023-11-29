using AirWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Repository
{
    public class EnvironmentDataRepository
    {
        public static List<TBL_ENVIRONMENTDATAOUT> GetTop100()
        {
            AirWatchDBEntities airWatchDBEntities = new AirWatchDBEntities();
            List<TBL_ENVIRONMENTDATA> top100Data = airWatchDBEntities.TBL_ENVIRONMENTDATA
                .OrderBy(env => env.EDID)
                .Take(100)
                .ToList();

            top100Data = top100Data.OrderBy(env => env.EDID).ToList();

            List<TBL_ENVIRONMENTDATAOUT> output = new List<TBL_ENVIRONMENTDATAOUT>();

            foreach(var data in top100Data)
            {
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

                output.Add(jsonData);
            }

            return output;
        }
        public static TBL_ENVIRONMENTDATA GetLatest()
        {
            AirWatchDBEntities airWatchDBEntities = new AirWatchDBEntities();
            TBL_ENVIRONMENTDATA latestData = airWatchDBEntities.TBL_ENVIRONMENTDATA
                .OrderByDescending(env => env.EDID).FirstOrDefault();

            return latestData;
        }

        public static List<TBL_ENVIRONMENTDATA> GetDataByDate(DateTime date)
        {
            DateTime currentDate = date.Date; // Get the current date without time
            DateTime nextDay = currentDate.AddDays(1); // Get the start of the next day

            //AirWatchDBEntities airWatchDBEntities = new AirWatchDBEntities();
            //List<TBL_ENVIRONMENTDATA> top100Data = airWatchDBEntities.TBL_ENVIRONMENTDATA
            //    .Where(env => env.CREATEDDATE >= currentDate && env.CREATEDDATE < nextDay)
            //    .OrderByDescending(env => env.CREATEDDATE)
            //    .ToList();

            //top100Data = top100Data.OrderBy(env => env.EDID).ToList();

            //return top100Data;
            using (AirWatchDBEntities airWatchDBEntities = new AirWatchDBEntities())
            {
                List<TBL_ENVIRONMENTDATA> top100Data = airWatchDBEntities.TBL_ENVIRONMENTDATA
                    .Where(env => env.CREATEDDATE >= currentDate && env.CREATEDDATE < nextDay)
                    .OrderByDescending(env => env.CREATEDDATE)
                    .ThenBy(env => env.EDID) // Use ThenBy for secondary sorting
                    //.Take(100) // Limit the result to the top 100 records
                    .ToList();

                return top100Data;
            }
        }
        public static List<TBL_ENVIRONMENTDATA> GetDataByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            DateTime currentDate = dateFrom.Date; // Get the current date without time
            DateTime nextDay = dateTo.Date; // Get the start of the next day

            AirWatchDBEntities airWatchDBEntities = new AirWatchDBEntities();
            List<TBL_ENVIRONMENTDATA> top100Data = airWatchDBEntities.TBL_ENVIRONMENTDATA
                .Where(env => env.CREATEDDATE >= currentDate && env.CREATEDDATE < nextDay)
                .OrderByDescending(env => env.CREATEDDATE)
                .ToList();

            top100Data = top100Data.OrderBy(env => env.EDID).ToList();

            return top100Data;
        }
    }
}