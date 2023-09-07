using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AirWatch.Models;

namespace AirWatch.Repository
{
    public class LoginRepository
    {
        public static TBL_ACCOUNTS GetAccountByUsernamePassword(string username, string password)
        {
            AirWatchDBEntities entities = new AirWatchDBEntities();
            var account = (from accounts in entities.TBL_ACCOUNTS.Where(acnt => acnt.USERNAME == username && acnt.PASSWORD == password) select accounts).FirstOrDefault();

            return (TBL_ACCOUNTS)account;
        }
        public static TBL_ACCOUNTS GetAccountByAccountID(Guid accountid)
        {
            AirWatchDBEntities entities = new AirWatchDBEntities();
            var account = (from accounts in entities.TBL_ACCOUNTS.Where(acnt => acnt.ACCOUNTID == accountid) select accounts).FirstOrDefault();

            return (TBL_ACCOUNTS)account;
        }
        public static TBL_ACCOUNTS GetAccountByEmail(string email)
        {
            AirWatchDBEntities entities = new AirWatchDBEntities();
            var account = (from accounts in entities.TBL_ACCOUNTS.Where(acnt => acnt.EMAIL == email) select accounts).FirstOrDefault();

            return (TBL_ACCOUNTS)account;
        }
    }
}