﻿using AirWatch.Models;
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
    }
}