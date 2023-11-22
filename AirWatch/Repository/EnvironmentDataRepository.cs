using AirWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Repository
{
    public class EnvironmentDataRepository
    {
        public static List<TBL_ENVIRONMENTDATA> GetTop100()
        {
            AirWatchDBEntities airWatchDBEntities = new AirWatchDBEntities();
            List<TBL_ENVIRONMENTDATA> top100Data = airWatchDBEntities.TBL_ENVIRONMENTDATA
                .OrderByDescending(env => env.EDID)
                .Take(100)
                .ToList();

            top100Data = top100Data.OrderBy(env => env.EDID).ToList();

            return top100Data;
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