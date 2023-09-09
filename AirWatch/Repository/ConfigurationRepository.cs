using AirWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Repository
{
    public class ConfigurationRepository
    {
        public static Guid? GetValidAPIkey()
        {
            AirWatchDBEntities entities = new AirWatchDBEntities();
            TBL_CONFIGURATION data = (TBL_CONFIGURATION)(from entdata in entities.TBL_CONFIGURATION.Where(config => config.CONFIGURATIONKEY == "APIKEY") select entdata).FirstOrDefault();

            return data.CONFIGURATIONVALUEGUID;
        }
    }
}