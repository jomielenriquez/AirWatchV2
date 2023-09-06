using AirWatch.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Models
{
    public class AppModel
    {
        public Guid AccountID {
            get 
            {
                if (HttpContext.Current.Session["AccountID"] != null)
                {
                    return new Guid(HttpContext.Current.Session["AccountID"].ToString());
                }
                return Guid.Empty;
            } 
        }
        public TBL_ACCOUNTS Account {
            get
            {
                if(HttpContext.Current.Session["currentUserAccount"] != null)
                {
                    return LoginRepository.GetAccountByAccountID(AccountID);
                    //return HttpContext.Current.Session["currentUserAccount"] as TBL_ACCOUNTS;
                }
                return null;
            } 
        }
        public ChangePass ChangePass { get; set; }
        public string Message {
            get
            {
                if (HttpContext.Current.Session["Message"] != null)
                {
                    return HttpContext.Current.Session["Message"] as string;
                }
                return null;
            }
        }
        public string MessageType {
            get
            {
                if (HttpContext.Current.Session["MessageType"] != null)
                {
                    return HttpContext.Current.Session["MessageType"] as string;
                }
                return null;
            }
        }
    }
    public class ChangePass
    {
        public string firstPass { get; set; }
        public string secondPass { get; set; }
    }
}