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
        public List<TBL_ENVIRONMENTDATA> Data { get; set; }
        public string DateFilter {
            get
            {
                return datefilter;
            }
            set 
            { 
                datefilter = value;
            }
        }
        private string datefilter = DateTime.Now.ToString("yyyy-MM-dd");
        public string DateFrom
        {
            get
            {
                return dateFrom;
            }
            set
            {
                dateFrom = value;
            }
        }
        private string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
        public string DateTo
        {
            get
            {
                return dateTo;
            }
            set
            {
                dateTo = value;
            }
        }
        private string dateTo = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        public List<ChartTypes> ChartTypes {
            get {
                return chartTypes;
            }
            set { 
                chartTypes = value;
            }
        }
        private List<ChartTypes> chartTypes = new List<ChartTypes>() {
            new ChartTypes()
            {
                ID = 1,
                Name = "Ammonia"
            },
            new ChartTypes()
            {
                ID = 2,
                Name = "Carbon Monoxide"
            },
            new ChartTypes()
            {
                ID = 3,
                Name = "Humidity"
            },
            new ChartTypes()
            {
                ID = 4,
                Name = "Nitrogen Oxide"
            },
            new ChartTypes()
            {
                ID = 5,
                Name = "Sulfur"
            },
            new ChartTypes()
            {
                ID = 6,
                Name = "Temperature"
            },
        };
        public int ChartTypeSelected { get; set; }
        public List<TBL_ENVIRONMENTDATA> readings { get; set; }
    }
    public class ChangePass
    {
        public string firstPass { get; set; }
        public string secondPass { get; set; }
    }
    public class ChartTypes
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}