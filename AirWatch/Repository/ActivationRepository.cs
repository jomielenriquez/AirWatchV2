using AirWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Repository
{
    public class ActivationRepository
    {
        public static string CreateActivationWithEmail(string email)
        {
            Data data = new Data();
            TBL_ACTIVATION newActivation = new TBL_ACTIVATION();
            newActivation.EMAIL = email;

            Random rand = new Random();
            int randomNumber = rand.Next(100000, 999999);

            newActivation.CODE = randomNumber.ToString();

            string result = data.Save(newActivation, new List<string> { "ACTIVATIONID" }, "ACTIVATIONID");
            if(new Guid(result) != Guid.Empty)
            {
                return newActivation.CODE;
            }
            return null;
        }
        public static bool CheckStatusWithEmail(string email)
        {
            AirWatchDBEntities entities = new AirWatchDBEntities();
            var data = (from account in entities.TBL_ACCOUNTS.Where(acnt => acnt.EMAIL == email) select account).FirstOrDefault();

            if (data == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}